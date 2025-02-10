<?php
    session_start();
    if ($_SESSION != null){
       header("Location: list.php");
    }
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="style.css" rel="stylesheet">
    <title>Регистрация</title>
    <link rel="preconnect" href="https://fonts.googleapis.com">
	<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300&display=swap" rel="stylesheet">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
</head>
<body>
    <form class="data"  method="POST">
            <header><p id="ppp">Регистрация</p></header>
            <button class="btn3" id="theme1"></button>
            <div class="yach">
                <label class="pp">Username</label>
                <input name = "psw1" type="text" id="username">
                
            </div>
            <div class="yach">
                <label class="pp">Password</label>
                <input name = "psw2" type="text" id="username">
            </div>
            <div class="yach">
                <label class="pp">Confirm password</label>
                <input type="text" id="username">
            </div>
        
        <div class="btns">
            <button name="btn" class="btn" id="Send">Submit</button>
            <button class="btn" id="Send">Reset</button>
        </div>

        <div class="down">
            <label class="pp">Already have an account? <a href="index.php"  class="pp">Sigh up now!</a></label>
        </div>
        <script src = "main.js"> </script> 
    </form>
</body>
</html>

<?php
    require "config.php";
    //проверка нажатия на кнопку
    if(isset($_POST["btn"]))
    {
        $login = $_POST["psw1"];
        $pwd = $_POST["psw2"];
        //запрос на добавление записи в базу данных
        $request = "INSERT INTO users(login, password) VALUES (?,?)";
        //отправляем данные в базу данных
        $result = $pdo->prepare($request);
        $result->execute([$login, $pwd]);
        // if($result) echo "<h1>Success</h1>";
        // else echo "Error";
        

        // $_SESSION['login'] = $res['login'];// структура ключ значение 
        // header("Location: list.php");
    }
    
    
?>