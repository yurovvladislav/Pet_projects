<?php
    require "config.php";
    $i = $_REQUEST['id'];
    $mn = $_REQUEST['meaning'];
    $request = "UPDATE tasks SET isDone = ? WHERE tasks.id = ?";
    $result = $pdo->prepare($request);
    $result->execute([$mn, $i]);
    header("Location: list.php");
?>