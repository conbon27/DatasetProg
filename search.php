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
    $DbConn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    $DbConn->setAttribute(PDO::MYSQL_ATTR_USE_BUFFERED_QUERY, true);
    $DbConn->setAttribute(PDO::ATTR_EMULATE_PREPARES, true);
    $DbConn->query('SET NAMES "utf8"');

} catch (Exception $e) {
}

//get query from index.php - if blank it shouldn't error out
$fetch = isset($_GET["fetch"]) ? $_GET["fetch"] : '';

// search keywords by user input
$results = array();

try{
  $STMT = $DbConn->prepare(
        "SELECT station, csvLink, jsonLink, keyword
        FROM DatasetInfo
        WHERE MATCH ( keyword)
        AGAINST ( :fetch IN NATURAL LANGUAGE MODE)"
    );
    $STMT->bindParam( 'fetch', $fetch,	PDO::PARAM_STR );
    $STMT->execute();


    while( $result	=	$STMT->fetch( PDO::FETCH_ASSOC ) ){

      //print print_r( $result,true );
        $results[] = $result;
    }
}
catch (Exception $e ) {

    //print print_r( $e,true );
    $results = array();
}

      $num_results = count($results);
//print ( print_r( $results,true));exit();
?>

<!DOCTYPE html>
<html lang="en-IE">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width = device-width, initial-scale = 1">
<link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css">
<title>Marine Institute Results</title>
</head>
<body>

<div class="container">

<div class ="page-header">

  <img src="Marine-Institutelogo.jpg">
</div>

<div class ="jumbotron">
  <p>
    <?php print $num_results;?> Result(s) Found.
    <div class = "row">
        <h1>Search Results</h1>

      <div class = "col-md-6">

    <table class="table table-bordered table-striped table-hover" id="productsTable">

        <thead>
        <tr>
            <th>Station Location</th>
            <th>CSV Data</th>
            <th>JSON Data</th>
        </tr>
        </thead>

        <tbody>
        <?php
        foreach ( $results as $numResults => $result){
            ?>

            <tr>
              <td>
                <?php print $result['station'];?>
              </td>
                <td>
                  <?php print '<a href="'.$result['csvLink'].'">CSV Data</a>';?>
                </td>
                <td>
                  <?php print '<a href="'.$result['jsonLink'].'">JSON Data</a>';?>
                </td>
            </tr>

            <?php
        } //endforeach
        ?>

        </tbody>
    </table>
  </div>
</div>
  </p>
</div>
</div>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
</body>
</html>
</body>
</html>
