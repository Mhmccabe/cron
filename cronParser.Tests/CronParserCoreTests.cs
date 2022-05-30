using System;
using Xunit;

namespace cronParser.Tests;

public class CronParserTests
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
        Assert.True( cfg.Minutes.Values.Count == 60);
    }

    [Fact]
    /// validate liited lists are represented correctly
    public void CheckValidCronJobEvery15an30()
    {
        var c = new cron.CronJob("15,30 * * * * ./task");
        var cfg = c.Config;

        Assert.True( c != null);
        Assert.True( cfg.Minutes.Values.Count == 2);
        Assert.True( cfg.Minutes.Values[0] == 15);
        Assert.True( cfg.Minutes.Values[1] == 30);

    }

    [Fact]
    /// validate paramtypesa re identified correctly
    public void CheckValidCronDataTypes()
    {
        var c = new cron.CronJob("15 1,2,3 1-2 * * ./task");
        var cfg = c.Config;

        Assert.True( c != null);
        Assert.True( cfg.Minutes.ParamType      == cron.CronParamTypeEnum.Value);
        Assert.True( cfg.Hours.ParamType        == cron.CronParamTypeEnum.List);
        Assert.True( cfg.DaysOfMonth.ParamType  == cron.CronParamTypeEnum.Range);
        Assert.True( cfg.Month.ParamType        == cron.CronParamTypeEnum.All);
        Assert.True( cfg.DaysOfWeek.ParamType   == cron.CronParamTypeEnum.All);

    }

    [Fact]
    public void CheckCalculation()
    {
        var c = new cron.CronJob("*/10 * * * * ./task");
        var cfg = c.Config;

        Assert.True(c != null);
        Assert.True(cfg.Minutes.Values.Count == 6);
        Assert.True(cfg.Minutes.Values[0] == 0);
        Assert.True(cfg.Minutes.Values[1] == 10);
        Assert.True(cfg.Minutes.Values[2] == 20);
        Assert.True(cfg.Minutes.Values[3] == 30);
        Assert.True(cfg.Minutes.Values[4] == 40);
        Assert.True(cfg.Minutes.Values[5] == 50);

    }
}