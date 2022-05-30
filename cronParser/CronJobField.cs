namespace cron
{
    ///holds the original passed parameter, its type and the interpelated values
    public class CronJobField {
        public string Param{ get; protected set; }
        public CronParamTypeEnum ParamType{ get; protected set; }
        public List<int> Values { get; protected set; }

        public CronJobField( string config, int minRange, int MaxRange )
        {
            this.Param = config;
            this.Values = new List<int>();
            FindcronParamType(config);
            CronParamToValues(minRange, MaxRange);
        }


        // determine the cron parameter type
        protected void FindcronParamType(string cronParam)
        {
            CronParamTypeEnum CurrentParamType = cron.CronParamTypeEnum.invalid;

            if( int.TryParse(cronParam, out _) )
                CurrentParamType = cron.CronParamTypeEnum.Value;
            else
                if (cronParam.Contains(','))
                    CurrentParamType = cron.CronParamTypeEnum.List;
                else
                if( cronParam.Equals("*" ) )
                    CurrentParamType = cron.CronParamTypeEnum.All;
                    else
                         if( cronParam.StartsWith("*/") )
                            CurrentParamType = cron.CronParamTypeEnum.Calculation;
                    else
                         if( cronParam.Contains('-') )
                            CurrentParamType = cron.CronParamTypeEnum.Range;
               
            this.ParamType = CurrentParamType;
        }


       // generate valid valiues based on the cron parameter type
        protected void CronParamToValues( int minRange, int MaxRange)
        {
            switch (this.ParamType)
            {
                case CronParamTypeEnum.All : {
                    for (int i=minRange; i<=MaxRange;i++)
                    {
                        this.Values.Add(i);
                    }
                    break;
                }
                case CronParamTypeEnum.List :{
                            string[] vals = this.Param.Split(",");
                            for ( int i =0; i < vals.Length; i++)
                            {
                                var v = int.Parse( vals[i] );
                                if( v >= minRange && v<= MaxRange)
                                    this.Values.Add( v );
                                else    
                                   throw new ArgumentException("list field");
                            }
                            break;
                        }
                case CronParamTypeEnum.Calculation: {
                    string[] vals =  this.Param.Split("/");
                    int stepSize = int.Parse( vals[1]);
                    for (int i=minRange; i<=MaxRange; i+=stepSize)
                    {
                        this.Values.Add(i);
                    }
                    break;
                }
                case CronParamTypeEnum.Range: {
                    string[] vals =  this.Param.Split("-");
                    int minVal = int.Parse( vals[0]);
                    int MaxVal = int.Parse( vals[1]);
                    for (int i=minVal; i<=MaxVal; i++)
                    {
                        if( i >= minRange && i <= MaxRange)
                            this.Values.Add(i);
                        else    
                            throw new ArgumentException("range field");
                    }
                    break;
                }
                case CronParamTypeEnum.Value: {
                   
                    int val = int.Parse( Param);
                    if( val >= minRange && val<= MaxRange)
                        this.Values.Add( val );
                    else    
                        throw new ArgumentException("Value field");
                    break;
                }

            }
        }

}



    }


     