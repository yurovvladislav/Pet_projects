<?php
//задаём параметры для подключения к бд
    define('hostName', "localhost");
    define('bdName', "rvp1");
    define('userName', "root");
    define('password', "");

    //создаём соединение с бд
    $pdo = new PDO('mysql:host='.hostName.';dbname='.bdName, userName, password);
?>