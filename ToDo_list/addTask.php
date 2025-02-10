<?php
    session_start();
    require "config.php";
    $task = $_POST['case'];
    $request = "INSERT INTO tasks(task, loginUser, isDone) VALUES (?,?,?)";
    $result = $pdo->prepare($request);
    $result->execute([$task, $_SESSION['login'], 0]);
    header("Location: list.php");
?>