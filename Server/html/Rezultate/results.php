<?php
if((isset($_GET["clasa"])))
{
	if(!is_dir($_GET["clasa"]))
		{
			mkdir($_GET["clasa"],0725);
			chown($_GET["clasa"],"www-data");
		}	
	$date = date("d.m.Y");
	if(!is_dir($_GET["clasa"] ."/". $date))
		{
			mkdir($_GET["clasa"] ."/".$date,0725);
			chown($_GET["clasa"] ."/".$date,"www-data");		
		}
	if( (isset($_GET["parola"])) and ($_GET["parola"] == "q.d1b3A2C"))
	{	
		if(isset($_GET["nume"]) and (isset($_GET["scor"])) )
		{
			$myfile = fopen($_GET["clasa"] ."/".$date . "/" . $_GET["nume"] . ".txt", 'w') or die('Cannot open file:  '.$_GET["clasa"]."/".$date . "/" . $_GET["nume"] . "txt"); //implicitly creates file
			$data = $_GET["scor"];
			fwrite($myfile, $data);
		}
	}
	else
	{
		header('Location: ../index.php');
	}
}
else
{
	header('Location: ../index.php');
}


?>
