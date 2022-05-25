using System;
using Xunit;

namespace cronParser.Tests;

public class cronParserTests
{
    [Fact]
    /// validate that a empty cron definition will fail to create a class
    public void CheckInvalidCronJob()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var c = new cron.CronJob("");
        });
    }

    [Fact]
    /// CHeck if a valid cron definition is passed it it parsed
    public void CheckValidCronJob()
    {
        var c = new cron.CronJob("* * * * * ./task");
        Assert.True( c != null);
    }

    [Fact]
    ///Validte all minutes inclide 0..60
    public void CheckValidCronJobEveryMin()
    {
        var c = new cron.CronJob("* * * * * ./task");
        var cfg = c.Config;

        Assert.True( c != null);
        Assert.True( cfg.Minutes.values.Count == 60);
    }

    [Fact]
    /// validate liited lists are represented correctly
    public void CheckValidCronJobEvery15an30()
    {
        var c = new cron.CronJob("15,30 * * * * ./task");
        var cfg = c.Config;

        Assert.True( c != null);
        Assert.True( cfg.Minutes.values.Count == 2);
        Assert.True( cfg.Minutes.values[0] == 15);
        Assert.True( cfg.Minutes.values[1] == 30);

    }

    [Fact]
    /// validate paramtypesa re identified correctly
    public void CheckValidCronDataTypes()
    {
        var c = new cron.CronJob("15 1,2,3 1-2 * * ./task");
        var cfg = c.Config;

        Assert.True( c != null);
        Assert.True( cfg.Minutes.ParamType      == cron.cronParamTypeEnum.Value);
        Assert.True( cfg.Hours.ParamType        == cron.cronParamTypeEnum.List);
        Assert.True( cfg.DaysOfMonth.ParamType  == cron.cronParamTypeEnum.Range);
        Assert.True( cfg.Month.ParamType        == cron.cronParamTypeEnum.All);
        Assert.True( cfg.DaysOfWeek.ParamType   == cron.cronParamTypeEnum.All);

    }

}