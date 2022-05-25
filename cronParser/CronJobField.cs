namespace cron
{
    ///holds the original passed parameter, its type and the interpelated values
    public class CronJobField {
        public string param{ get; protected set; }
        public cronParamTypeEnum ParamType{ get; protected set; }
        public List<int> values{ get; protected set; } 
 
        public CronJobField( string config, int minRange, int MaxRange )
        {
            this.param = config;
            this.values = new List<int>();
            FindcronParamType(config);
            CronParamToValues(config, minRange, MaxRange);
        }


        // determine the cron parameter type
        protected void FindcronParamType(string cronParam)
        {
            cronParamTypeEnum CurrentParamType = cron.cronParamTypeEnum.invalid;

            if( int.TryParse(cronParam, out _) )
                CurrentParamType = cron.cronParamTypeEnum.Value;
            else
                if (cronParam.Contains(","))
                    CurrentParamType = cron.cronParamTypeEnum.List;
                else
                if( cronParam.Equals("*" ) )
                    CurrentParamType = cron.cronParamTypeEnum.All;
                    else
                         if( cronParam.StartsWith("*/") )
                            CurrentParamType = cron.cronParamTypeEnum.Calculation;
                    else
                         if( cronParam.Contains("-") )
                            CurrentParamType = cron.cronParamTypeEnum.Range;
               
            this.ParamType = CurrentParamType;
        }


       // generate valid valiues based on the cron parameter type
        protected int[] CronParamToValues(string CronParam, int minRange, int MaxRange)
        {
            switch (this.ParamType)
            {
                case cronParamTypeEnum.All : {
                    for (int i=minRange; i<=MaxRange;i++)
                    {
                        this.values.Add(i);
                    }
                    break;
                }
                case cronParamTypeEnum.List :{
                            string[] vals = this.param.Split(",");
                            for ( int i =0; i < vals.Length; i++)
                            {
                                var v = int.Parse( vals[i] );
                                if( v >= minRange && v<= MaxRange)
                                    this.values.Add( v );
                                else    
                                   throw new ArgumentException();
                            }
                            break;
                        }
                case cronParamTypeEnum.Calculation: {
                    string[] vals =  this.param.Split("/");
                    int stepSize = int.Parse( vals[1]);
                    for (int i=minRange; i<=MaxRange; i+=stepSize)
                    {
                        this.values.Add(i);
                    }
                    break;
                }
                case cronParamTypeEnum.Range: {
                    string[] vals =  this.param.Split("-");
                    minRange = int.Parse( vals[0]);
                    MaxRange = int.Parse( vals[1]);
                    for (int i=minRange; i<=MaxRange; i++)
                    {
                        this.values.Add(i);
                    }
                    break;
                }
                case cronParamTypeEnum.Value: {
                   
                    int val = int.Parse( param);
                    if( val >= minRange && val<= MaxRange)
                        this.values.Add( val );
                    else    
                        throw new ArgumentException();
                    break;
                }

            }
            

            return new List<int>().ToArray();
        }

}



    }


     