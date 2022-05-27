using System;
using System.Collections.Generic;


namespace cron
{
    /// core class that represents the cron parser
    public class CronJob
    {
        readonly string[] specParams;

        public CronJob( string cronSpec )
        : base()
        {   
            specParams = cronSpec.Split(" ");
            if(specParams.Length != 6)
            {
                string msg = string.Format( "invalid cron expression p:{0}", specParams.Length);
                throw new ArgumentException(msg);
            }
            this.Config = new CronConfig( cronSpec );    

        }
        // readonly representation of the config for this cron job
        public CronConfig Config { get; protected set; }



        // preturn a formated string that represent the cron configuration
        public string PrettyPrint()
        {
            List<string> msg = new()
            {
                String.Format("{0,-20}{1}", "minute", string.Join(", ", Config.Minutes.Values)),
                String.Format("{0,-20}{1}", "Hour", string.Join(", ", Config.Hours.Values)),
                String.Format("{0,-20}{1}", "Day of month", string.Join(", ", Config.DaysOfMonth.Values)),
                String.Format("{0,-20}{1}", "Month", string.Join(", ", Config.Month.Values)),
                String.Format("{0,-20}{1}", "Day of week", string.Join(", ", Config.DaysOfWeek.Values)),
                String.Format("{0,-20}{1}", "Command", Config.command)
            };
            return string.Join("\n", msg);
        }

    }
}