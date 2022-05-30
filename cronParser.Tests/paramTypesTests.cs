using System;
using cron;
using Xunit;

namespace cronParser.Tests;

public class ParamtypesTests
{
    [Fact]
    public void WilCardParamtype()
    {
      var sut = new CronJobField("*", 1, 2);
      Assert.True(sut.ParamType == cron.CronParamTypeEnum.All);
      Assert.True(sut.Values.Count == 2);
    }

    [Fact]
    public void RangeParamtype()
    {
      var sut = new CronJobField("1-2", 1, 2);
      Assert.True(sut.ParamType == cron.CronParamTypeEnum.Range);
    }
   
    [Fact]
    public void ValuesParamtype()
    {
      var sut = new CronJobField("1,2", 1, 2);
      Assert.True(sut.ParamType == cron.CronParamTypeEnum.List);
    }

    [Fact]
    public void ValueParamType()
    {
      var sut = new CronJobField("1", 1, 2);
      Assert.True(sut.ParamType == cron.CronParamTypeEnum.Value);
    }

    [Fact]
    public void InvalidaramType()
    {
      var sut = new CronJobField("x", 1, 2);
      Assert.True(sut.ParamType == cron.CronParamTypeEnum.invalid);
    }

    [Fact]
    /// validates non of the listed valies are outside the valid range
    public void CheckInvalidListfieldCronJob()
    {
        _ = Assert.Throws<ArgumentException>(() =>
        {
            var sut = new CronJobField("1,5", 1, 2);
        });
    }

    [Fact]
    /// validate single value cannot be outside of the valid range
    public void CheckInvalidValuefieldCronJob()
    {
        Assert.Throws<ArgumentException>(() =>
        {
          var sut = new CronJobField("5", 1, 2);
        });
    }

}