<?php
    session_start();
    if ($_SESSION == null){
       header("Location: index.php");
    }
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="style.css" rel="stylesheet">
    <title>Список дел</title>
    <link rel="preconnect" href="https://fonts.googleapis.com">
	<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300&display=swap" rel="stylesheet">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
</head>
<body>
    <header>
        <p id="ppp">Список дел</p>
    </header>
    <button class="btn3" id="theme1"></button>
    
    <div class = "list"  method = "POST">
        <?php
        require "config.php";
        $request = "SELECT * FROM tasks WHERE loginUser = ?";
        $result = $pdo->prepare($request); 
        $result->execute([$_SESSION['login']]); 
        global $id, $id2;
        $id = 0;
        $cs = 0;
        foreach ($result as $res)
        {
            if ($res['isDone'] == 0)
            {
                echo '<div class="c">
                <div class="txt">
                    <button class = "notupdated"></button>
                    <label><a href = "fullUpdate.php?id='.$res['id'].'&meaning=1"><input type="checkbox" class="custom" data-attr = "0"></a>'.$res['task'].'</label>
                </div> 
                <div class="phs">  
                <img class = "ph" src = "1.png" data-attr = '.$res['id'].' > 
                <a href = "deleteTask1.php?id='.$res['id'].'" ><img class = "ph2" src = "2.png"></a>               
                </div>
                </div>';
            }
            elseif ($res['isDone'] == 1)
            {
                echo '<div class="c">
                <div class="txt">
                    <button class = "updated"></button>
                    <label><a href = "fullUpdate.php?id='.$res['id'].'&meaning=0"><input type="checkbox" class="custom" data-attr = "1" checked></a>'.$res['task'].'</label>
                </div> 
                <div class="phs">  
                <img class = "ph" src = "1.png" data-attr = '.$res['id'].' > 
                <a href = "deleteTask1.php?id='.$res['id'].'" ><img class = "ph2" src = "2.png"></a>               
                </div>
                </div>';
            }
            $cs++;
        }     
        echo '<div id = "hg" data-attr = '.$cs.'></div>';   
        ?>
    </div>
    <div class = "btns">
        <button class="btn" id="Send">Добавить</button>
        <a href = "logout.php"><button class="btn2" id="Save">Выйти</button></a>
        
    </div>
    <div class = "modal">
            <span class = "close">&times;</span>
			<form class = "mdl"  action = "addTask.php" method = "POST">
                <input name = "case" type="text" info = "case">
                <input class="btn" type = "submit" id="Write" name="Write" value = "Добавить">
            </form>
    </div>
    <div class = "modal2">
            <span class = "close">&times;</span>
			<form class = "mdl"  action = "changeTask.php" method = "POST">
                <input name = "cas" type="text" info = "case">
                <input class = "hid" type = "hidden" name = 'q'>
                <input class = "btn" type = "submit" id="Write2" name="Write2" value = "Изменить">
            </form>
    </div>   
</body>
<script src = "main.js"> </script>
</html>
<!-- echo "<h1>".$res['login']."</h1>" -->
