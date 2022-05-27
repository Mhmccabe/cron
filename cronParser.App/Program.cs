using cron;
// See https://aka.ms/new-console-template for more information


try
{
    var c = new cron.CronJob( args[0] );
    Console.WriteLine( c.PrettyPrint() );
}
catch (ArgumentException e)
{
    Console.WriteLine( "invallid command line : {0}", e.Message) ;
}
catch( Exception)
{
    Console.WriteLine( "please pass a single paramerter with the cron expression") ;

}