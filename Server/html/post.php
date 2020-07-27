<?php $seed = 100 ?>

<?php
	session_start();
	$_SESSION['succes']="";

if (!isset($_SESSION['loggedin'])) {
	header('Location: index.php');
	exit();
}

    if($_SERVER['REQUEST_METHOD'] == "POST" and isset($_POST['trimis']) and $_POST['trimis'] == "Set")
    {
		
        func();
    }
    function func()
    {
		$_SESSION['succes'] = "Valori setate cu succes";
        $file = 'seed.txt';
		$current = file_get_contents($file);
		$current = $_POST["seed"];
		file_put_contents($file, $current);
		
		$file = 'email.txt';
		$current = file_get_contents($file);
		$current = $_POST["mail"];
		file_put_contents($file, $current);
		
		$file = 'clasa.txt';
		$current = file_get_contents($file);
		$current = $_POST["clasa"];
		file_put_contents($file, $current);
		
    }
?>

<html>

<head>
<!--<script src="jQuery.js"></script> -->
<!--<script src="script.js"></script> -->
</head>
<link href="style2.css" rel="stylesheet" type="text/css">

<div class="login">
<form id="detalii" action="" method="post">
	
	<h1 text-align: center;> Detalii test</h1>
	<input type="text" name="clasa" placeholder="Clasa"><br>
    <input type="text" name="seed" placeholder="Seed"><br>
	<input type="text" name="mail" placeholder="Email"><br>
    <input type="submit" name="trimis" value="Set" />
	<?php
	if($_SESSION['succes'])?>
			<h3 text-align: center;color="green"><?php echo($_SESSION['succes']) ?></h3>
			<?php
	?>
	
</form>
</div>




</html>
