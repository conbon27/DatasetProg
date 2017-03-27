<?php


//try to establish a database connection
try {

    //create new database object
    $DbConn = new PDO(
        'mysql:http://danu6.it.nuigalway.ie//;port=3306;dbname=mydb2463;',
        'mydb2463ca',
        'mi3tax',
        array(PDO::ATTR_PERSISTENT => false)
    );
    //make the database layer throw exceptions on error
    $DbConn->setAttribute( PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION );
    $DbConn->setAttribute( PDO::MYSQL_ATTR_USE_BUFFERED_QUERY, true );
    $DbConn->setAttribute( PDO::ATTR_EMULATE_PREPARES, true );
    $DbConn->query('SET NAMES "utf8"');

}
catch (Exception $e) {}



$dir ="/home/danu_ca9/files/";
$a = scandir($dir);
foreach ($a as $key => $value) {
    $pos = strpos($value, '.jsonld');
    if ($pos === false) {
        //nothing
    }
    else
    {
        //echo $value;
        $ext = pathinfo($value, PATHINFO_EXTENSION);
        if($ext=='jsonld')
        {
            $file_contents = file_get_contents($dir.$value);
		        $json= json_decode($file_contents, true);
            $keywords = $json[0]['keywords'];
            $csv = $json[0]['distribution'][1]['contentUrl'];
            $js = $json[0]['distribution'][0]['contentUrl'];

            $keywordsStr = "";
            foreach ($keywords as $keywordsK => $keywordsV) {

              $keywordsStr .= $keywordsV . " ";
            }

            $keywordsStr = strtolower( $keywordsStr );
            $keywordsStr = preg_replace('/[^a-z0-9]+/i', ' ', $keywordsStr);
            $keywordsStr = preg_replace('/\s+/', ' ',$keywordsStr);

            //insert
            try {

                $STMT = $DbConn->prepare(
                    "INSERT INTO DatasetInfo (
                        keyword,
                        content,
                        csvLink,
                        jsonLink
                    ) VALUES (
                        :keyword,
                        :content,
                        :csvLink,
                        :jsonLink
                    )"
                );
                $STMT->bindParam( 'keyword', $keywordsStr , PDO::PARAM_STR );
                $STMT->bindParam( 'content', $file_contents, PDO::PARAM_STR );
                $STMT->bindParam('csvLink', $csv, PDO::PARAM_STR);
                $STMT->bindParam('jsonLink', $js, PDO::PARAM_STR);
                $STMT->execute();

            }
            catch (Exception $e) {
              print ( print_r( $e,true));exit();
            }

            print $dir.$value."\n";
            //print ( print_r( $keywordsStr,true));exit();
        }
    }
}






?>
