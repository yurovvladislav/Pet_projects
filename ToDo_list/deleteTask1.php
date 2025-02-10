<?php
require "config.php";
$i = $_REQUEST['id'];
$request = "DELETE FROM tasks WHERE id = ?";
$result = $pdo->prepare($request);
$result->execute([$i]);
header("Location: list.php");
?>