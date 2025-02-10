<?php
    require "config.php";
    $id2 = $_POST['q'];
    $nw = $_POST['cas'];  
    $request = "UPDATE tasks SET task = ? WHERE tasks.id = ?";
    $result = $pdo->prepare($request);
    $result->execute([$nw, $id2]);  
    header("Location: list.php");
    //header( "Refresh: URL=list.php" );
?>