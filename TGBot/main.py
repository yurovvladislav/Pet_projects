from aiogram import Bot, Dispatcher, types
from aiogram.types import ReplyKeyboardMarkup, KeyboardButton, InlineKeyboardMarkup, InlineKeyboardButton
from aiogram.utils.executor import start_polling
import asyncio
from aiogram import Bot, Dispatcher, types
import torch
from transformers import BertTokenizer
import numpy as np
import pandas as pd
import torch.nn as nn
from sklearn.preprocessing import LabelEncoder
from translate import Translator
import deepl
from googletrans import Translator
from deep_translator import GoogleTranslator
import sqlite3
import os
import re

# Токен вашего бота
API_TOKEN = '7188448507:AAFdLIDO_aj2LUsyw1AP1nMPNiW6wJEmBLA'

# Укажите ID пользователя, которому доступны кнопки
# AUTHORIZED_USER_ID = 812020449  # Замените на ваш Telegram ID
AUTHORIZED_USER_ID = 1182354739  # Замените на ваш Telegram ID

# Инициализация бота и диспетчера
bot = Bot(token=API_TOKEN)
dp = Dispatcher(bot)
chat_id = -1002447282464

# путь к модели и бд
route = r"C:\Users\Vladislav\Downloads\TGBot\TGBot\chat_model.pth"
db_route = r"C:\Users\Vladislav\Downloads\TGBot\TGBot\bot_database.db"

# Основная клавиатура
main_keyboard = ReplyKeyboardMarkup(resize_keyboard=True)
main_keyboard.add(KeyboardButton("Просмотр статистики"))
main_keyboard.add(KeyboardButton("Предупреждения"))

bad_words = ["негодяй", "простофиля", "блять", "мудак", "тварь", "чурка"] # примеры нецензурной лексики
def blur_bad_words(text):
    for word in bad_words:
        text = text.lower().replace(word, '*' * len(word))  
    return text

# ///////////////////////////// ИИ модуль ////////////////////////////////////////

# Загружаем модель и токенизатор
class ChatModel(nn.Module):
    def __init__(self, n_classes):
        super(ChatModel, self).__init__()
        from transformers import BertModel
        self.bert = BertModel.from_pretrained('bert-base-uncased')
        self.dropout = nn.Dropout(0.3)
        self.fc = nn.Linear(self.bert.config.hidden_size, n_classes)

    def forward(self, input_ids, attention_mask):
        output = self.bert(input_ids=input_ids, attention_mask=attention_mask)
        pooled_output = output.pooler_output
        output = self.dropout(pooled_output)
        output = self.fc(output)
        return output

# Функция для загрузки модели
def load_model(model_path, n_classes):
    model = ChatModel(n_classes)
    model.load_state_dict(torch.load(model_path))
    model = model.cuda() if torch.cuda.is_available() else model
    model.eval()
    return model

# Функция для предсказания эмоции
def predict_emotion(model, tokenizer, text, label_encoder, max_len=128):
    # Токенизация текста
    encoding = tokenizer(text, truncation=True, padding='max_length', max_length=max_len, return_tensors='pt')

    input_ids = encoding['input_ids'].cuda() if torch.cuda.is_available() else encoding['input_ids']
    attention_mask = encoding['attention_mask'].cuda() if torch.cuda.is_available() else encoding['attention_mask']

    # Прогоняем через модель
    with torch.no_grad():
        outputs = model(input_ids, attention_mask)
        _, predicted = torch.max(outputs, 1)

    # Преобразуем предсказание в оригинальную метку эмоции
    predicted_label = label_encoder.inverse_transform([predicted.item()])
    return predicted_label[0]

def predict(message):
    # Загружаем токенизатор
    tokenizer = BertTokenizer.from_pretrained('bert-base-uncased')
    label_encoder = LabelEncoder()
    label_encoder.fit(['0', '1', '2', '3', '4', '5', '6'])  
    
    model = load_model(route, len(label_encoder.classes_))

    # Переводим с русского на английский
    text = GoogleTranslator(source='ru', target='en').translate(message)

    # Получаем предсказание эмоции
    predicted_emotion = predict_emotion(model, tokenizer, text, label_encoder)

    return predicted_emotion

# Функция для проверки наличия нецензурных слов в сообщении
def contains_bad_words(text):
    for word in bad_words:
        if word.lower() in text.lower():  
            return True
    return False

def blur_bad_words(text):
    for word in bad_words:
        pattern = re.compile(re.escape(word), re.IGNORECASE)
        text = pattern.sub('*' * len(word), text)
    return text

#///////////////// Блок функций для БД ////////////////////////////////////////

def get_db_connection():
    conn = sqlite3.connect(db_route, check_same_thread=False) 
    return conn

# Функция для создания таблиц в базе данных
# def create_tables():
#     conn = get_db_connection()
#     c = conn.cursor()
#     c.execute('''
#         CREATE TABLE IF NOT EXISTS users (
#             id INTEGER PRIMARY KEY,
#             username TEXT,
#             full_name TEXT,
#             warnings INTEGER DEFAULT 0
#         )
#     ''')
# CREATE TABLE stat (
#     id         INTEGER NOT NULL
#                        PRIMARY KEY,
#     first_name TEXT,
#     id_user    INTEGER,
#     warnings   INTEGER
# );

#     conn.commit()
#     conn.close()

# Функция для добавления или обновления пользователя в базе данных
def add_or_update_user(user_id, first_name):
    conn = get_db_connection()
    c = conn.cursor()
    c.execute('SELECT * FROM warnings WHERE user_id = ?', (user_id,))
    user = c.fetchone()
    count = get_warnings(user_id)
    if user is None:
        c.execute('INSERT INTO warnings (user_id, first_name, warnings) VALUES (?, ?, ?)', (user_id, first_name, 1))
    else:
        count += 1
        c.execute('UPDATE warnings SET warnings = ? WHERE user_id = ?', (count, user_id))
    conn.commit()
    conn.close()

# Функция для получения предупреждений пользователя
def get_warnings(user_id):
    conn = get_db_connection()
    c = conn.cursor()
    c.execute('SELECT warnings FROM warnings WHERE user_id = ?', (user_id,))
    warnings = c.fetchone()
    conn.close()
    return warnings[0] if warnings else 0


# ///////////////////////// Основные обработчики ///////////////////////////////////
# Обработчик команды /start
@dp.message_handler(commands=['start'])
async def start_command(message: types.Message):
    if message.from_user.id == AUTHORIZED_USER_ID:
        await message.reply("Выберите действие:", reply_markup=main_keyboard)
    else:
        await message.reply("Отсутствие доступа.")

# Обработчик кнопки "Просмотр статистики"
@dp.message_handler(lambda message: message.text == "Просмотр статистики")
async def view_statistics(message: types.Message):
    count = get_warnings(message.from_user.id)
    try:
        # Формируем статистику пользователя
        stats_text = (
            "Вот ваша статистика:\n"
            f"- Негативных сообщений: {count}"
        )

        # Отправляем сообщение с клавиатурой в ЛС
        await bot.send_message(
            chat_id=message.from_user.id,
            text=stats_text,
        )
        await message.reply("Статистика отправлена вам в личные сообщения.")
    except Exception as e:
        await message.reply(f"Не удалось отправить статистику.\nОшибка: {e}")

# Обработчик кнопки "Предупреждения"
@dp.message_handler(lambda message: message.text == "Предупреждения")
async def warnings(message: types.Message):
    if message.from_user.id != AUTHORIZED_USER_ID:
        await message.reply("Отсутствие доступа.")
        return

    try:
        # Получаем список участников чата
        chat_members = await bot.get_chat_administrators(message.chat.id)
        # Создаём динамическую клавиатуру
        user_buttons = InlineKeyboardMarkup(row_width=2)
        for member in chat_members:
            # Формируем имя и фамилию пользователя
            user_first_name = member.user.first_name or "Без имени"
            user_last_name = member.user.last_name or ""
            full_name = f"{user_first_name} {user_last_name}".strip()

            # Добавляем кнопку с именем и фамилией
            user_buttons.add(InlineKeyboardButton(text=full_name, callback_data=f"warn_{member.user.id}_{full_name}"))

        # Отправляем кнопки только авторизованному пользователю в ЛС
        await bot.send_message(
            chat_id=AUTHORIZED_USER_ID,
            text="Выберите пользователя для просмотра предупреждений:",
            reply_markup=user_buttons
        )
        await message.reply("Кнопки отправлены вам в личные сообщения.")
    except Exception as e:
        await message.reply(f"Не удалось получить список участников. Убедитесь, что бот — администратор.\nОшибка: {e}")

# Обработчик нажатий на кнопки с пользователями
@dp.callback_query_handler(lambda call: call.data.startswith("warn_"))
async def handle_user_warning(call: types.CallbackQuery):
    # Разбираем callback_data: warn_user_id_user_full_name
    _, user_id, user_full_name = call.data.split("_", 2)
    count = get_warnings(user_id)

    # Отправляем предупреждение в личный чат пользователя
    try:
        # Получаем объект пользователя по его ID
        user_id = int(user_id)

        # Формируем текст предупреждения
        warning_text = f"Уважаемый {user_full_name}, для вас было сгенерировано предупреждение\n Кол-во негативных сообщений - {count} \n Будьте добрее, попейте чаю. \n Хорошего вам дня!"

        # Отправляем сообщение пользователю
        await bot.send_message(chat_id=user_id, text=warning_text)

        # Ответ на кнопку, закрытие всплывающего окна
        await call.answer(f"Предупреждение отправлено пользователю {user_full_name}.")
    except Exception as e:
        await call.answer(f"Ошибка при отправке предупреждения_ _ _: {e}", show_alert=True)


# Обработчик всех входящих сообщений
@dp.message_handler(content_types=['text'])
async def check_toxic_behaviour(message: types.Message):
    emotion = int(predict(message.text)) # считываем негативную эмоцию
    last_user_id = message.from_user.id
    name = message.from_user.full_name
    count = get_warnings(message.from_user.id)
    try:
        if emotion >= 2:
            warning_text = f"Уважаемый {name}, для вас было сгенерировано предупреждение\n Кол-во негативных сообщений - {count} \n Будьте добрее, попейте чаю. \n Хорошего вам дня!"

            # Отправляем сообщение пользователю
            add_or_update_user(last_user_id, message.from_user.full_name) 
            await bot.send_message(chat_id=last_user_id, text=warning_text)


        if contains_bad_words(message.text):
            warning_text = f"Уважаемый {name}, для вас было сгенерировано предупреждение\n Кол-во негативных сообщений - {count} \n Пожалуйста, избегайте использования нецензурной лексики! Будьте добрее, попейте чаю. \n Хорошего вам дня!"
            blurred_message = blur_bad_words(message.text)
            # if blurred_message != message.text:
            #     warning_text = f"Уважаемый {name}, ваше сообщение было заблюрено из-за использования нецензурной лексики."
            #     await bot.send_message(chat_id=last_user_id, text=warning_text)
            if blurred_message != message.text:
                warning_text = f"Уважаемый {message.from_user.first_name}, ваше сообщение было заблюрено из-за использования нецензурной лексики."
                await message.delete()
                await bot.send_message(chat_id=chat_id, text=f'By {name}')
                await message.answer(blurred_message)
            # Отправляем сообщение пользователю
            add_or_update_user(last_user_id, message.from_user.full_name)  
            await bot.send_message(chat_id=chat_id, text=warning_text)
            
    except Exception as e:
        # await call.answer(f"Ошибка при отправке предупреждения: {e}", show_alert=True)
        await bot.send_message(chat_id=chat_id, text="Ошибка при отправке предупреждения___")

if __name__ == '__main__':
    print("Бот запущен...")
    start_polling(dp, skip_updates=True)
