<?php
    session_start();
    $_SESSION = array();//присваиваем null 
    session_destroy();
    header("Location: index.php");
?>