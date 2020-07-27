




<html>
	<head>
		<meta charset="utf-8">
		<title>Login</title>
		<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.1/css/all.css">
		<link href="style2.css" rel="stylesheet" type="text/css">
	</head>
	<body>
		<div class="login">
			<h1>Login</h1>
			<form action="<?php echo (isset($variable))?$variable:'';?>" method="post">
				<label for="username">
					<i class="fas fa-user"></i>
				</label>
				<input type="text" name="username" placeholder="Username" id="username" required>
				<label for="password">
					<i class="fas fa-lock"></i>
				</label>
				<input type="password" name="password" placeholder="Password" id="password" required>
				<input type="submit" value="Login" name="buton">
			</form>
		</div>
	</body>
</html>


<?php
	 session_start();
	 if($_SERVER['REQUEST_METHOD'] == "POST" and isset($_POST['buton']))
	 {
		 
		if(isset($_POST['username']) &&  $_POST['username'] == "Profesor" && isset($_POST['password']) &&  $_POST['password'] == "Parola")
		{
			header("Location: post.php");
			$_SESSION['loggedin'] = TRUE;
		}
		else
			header("Location: index.php");
		

	 }
	 
?>

