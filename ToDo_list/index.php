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
    <title>Авторизация</title>
    <link rel="preconnect" href="https://fonts.googleapis.com">
	<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300&display=swap" rel="stylesheet">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
</head>
<body>
        <form class="data" method="POST">
            <header><p id="ppp">Вход</p></header>
            <button class="btn3" id="theme1"></button>
            <div class="yach">
                <label class="pp">Username</label>
                <input name = "login" type="text" id="username">
                
            </div>
            <div class="yach">
                <label class="pp">Password</label>
                <input name = "psw" type="text" id="username">
            </div>
            <div class="btns">
                <input type="submit" name = "signIn" class="btn" id="Send" value = "Login">
            </div>
            <div class="down">
                <label class="pp">Don't have an account?<a href="registration.php" class="pp">Login here!</a></label> 
            </div>
            <script src = "main.js"> </script>  
</form>
       

        
</body>
</html>

<?php
require "config.php";
if(isset($_POST["signIn"]))
{
    $login = $_POST["login"];
    $pwd = $_POST["psw"];
    $request = "SELECT * FROM `users` WHERE login = ? AND password = ?";
    $result = $pdo->prepare($request);//gotovit
    $result->execute([$login, $pwd]);//posylaet
    if($res = $result->fetch()) {
        session_start();
        $_SESSION['login'] = $res['login'];// структура ключ значение 
        header("Location: list.php");
        //echo $_SESSION['login'];
    }
}
?>