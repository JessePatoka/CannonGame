<?php
    // Send variables for the MySQL database class.
    $database = mysql_connect('localhost', 'sileoloz_cannon', '5LDZ*u8#0GAV') or die('Could not connect: ' . mysql_error());
    mysql_select_db('sileoloz_cannongame') or die('Could not select database');

    $query = "SELECT * FROM `scores` ORDER by `score` DESC LIMIT 15";
    $result = mysql_query($query) or die('Query failed: ' . mysql_error());

    $num_results = mysql_num_rows($result);

    for($i = 0; $i < $num_results; $i++)
    {
         $row = mysql_fetch_array($result);
         echo $row['name']. "," . $row['xpos'] . "," . $row['ypos'] . "\n";
    }
?>