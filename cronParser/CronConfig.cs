namespace cron
{
    /// holds the parsed cron job definition
    public class CronConfig {

        public CronConfig( string cronSpec )
        {
            var specParams = cronSpec.Split(" ");
         
            this.Minutes     = new CronJobField( specParams[0], 0, 59 );
            this.Hours       = new CronJobField( specParams[1], 0, 23 );
            this.DaysOfMonth = new CronJobField( specParams[2], 1, 31);
            this.Month       = new CronJobField( specParams[3], 1, 12);
            this.DaysOfWeek  = new CronJobField( specParams[4], 0,  6);
            this.command     = specParams[5]; 
        }
        public CronJobField Minutes { get; protected set; }
        public CronJobField Hours { get; protected set; }
        public CronJobField DaysOfMonth { get; protected set; }
        public CronJobField Month { get; protected set; }
        public CronJobField DaysOfWeek { get; protected set; }
        public string command = "";
    }
}