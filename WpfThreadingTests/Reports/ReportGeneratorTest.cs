using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using WpfThreading.Db;
using System.Threading;
using WpfThreading.Reports;
using WpfThreading.Services;
using WpfThreading.Entities;

namespace WpfThreadingTests.Reports
{
    public class ReportGeneratorTest
    {
        [Fact]
        public void ErweiterteGoodSyncLogsTest()
        {
            // given
            var aktivitaetszeitraeume = new Aktivitaetszeitraeume()
                .Add(1000, "2015-10-01", "2015-10-02")
                .Add(2000, "2015-10-02", "2015-10-03")
                .Eintraege;

            var cancellationTokenStub = new CancellationToken();

            IEnumerable<ImportStatisticAggdEintrag> impstat =
                new ImportStatisticAggd()
                //   cid   zaehltag      beginDateMin           endDateMax             importDateMax          nDebug  nEintraege
                .Add(1000, "2015-10-01", "2015-10-01 00:00:00", "2015-10-01 23:59:00", "2015-10-03 05:00:00", 100, 1)
                .Add(1000, "2015-10-02", "2015-10-02 00:00:00", "2015-10-02 23:59:00", "2015-10-04 05:00:00", 101, 2)
                .Add(2000, "2015-10-03", "2015-10-03 00:00:00", "2015-10-03 23:59:00", "2015-10-05 05:00:00", 200, 4)
                .Eintraege;

            IEnumerable<ExportStatisticEintrag> expstat =
                new ExportStatistic()
                //   cid   zaehltag      beginDate              endDate                exportDate             fileIdx
                .Add(1000, "2015-10-01", "2015-10-01 00:00:00", "2015-10-01 23:59:00", "2015-10-03 08:00:00", 1)
                .Add(2000, "2015-10-03", "2015-10-03 00:00:00", "2015-10-03 23:59:00", "2015-10-05 08:00:00", 1)
                .Eintraege;

            IEnumerable<ErweiterterGoodSyncLogEintrag> expected =
                new ErweiterteGoodSyncLogs()
                //   cid   zaehltag      isBeginDateMin         isEndDateMax           isImportDateMax        isNDebugTurnusSum  isNEintraege  esBeginDate            esEndDate              esExportDate           esFileIdx
                .Add(1000, "2015-10-01", "2015-10-01 00:00:00", "2015-10-01 23:59:00", "2015-10-03 05:00:00", 100, 1, "2015-10-01 00:00:00", "2015-10-01 23:59:00", "2015-10-03 08:00:00", 1)
                .Add(1000, "2015-10-02", "2015-10-02 00:00:00", "2015-10-02 23:59:00", "2015-10-04 05:00:00", 101, 2, null, null, null, null)
                .Add(2000, "2015-10-03", "2015-10-03 00:00:00", "2015-10-03 23:59:00", "2015-10-05 05:00:00", 200, 4, "2015-10-03 00:00:00", "2015-10-03 23:59:00", "2015-10-05 08:00:00", 1)
                .Eintraege;

            var dbGatewayMock = new Mock<IDbGateway>();

            dbGatewayMock
                .Setup(g => g.Public_BinaryData_Import_Statistic_Aggd_Async(aktivitaetszeitraeume, cancellationTokenStub))
                .ReturnsAsync(impstat);

            dbGatewayMock
                .Setup(g => g.Public_BinaryData_Export_Statistic_Async(aktivitaetszeitraeume, cancellationTokenStub))
                .ReturnsAsync(expstat);

            var sut = new ReportGenerator(dbGatewayMock.Object);

            // when
            var t = sut.ErweiterteGoodSyncLogsAsync(
                aktivitaetszeitraeume, cancellationTokenStub);
            var actual = t.Result;

            // then
            Assert.Equal<ErweiterterGoodSyncLogEintrag>(expected, actual);
        }
    }

    public class MyAssertTest
    {
        [Fact]
        public void EqualTest_Equal_Objects()
        {
            // given
            var imp1 = new ImportStatisticAggdEintrag() { Cid = 1000, Zaehltag = new DateTime(2015, 1, 1) };
            var imp2 = new ImportStatisticAggdEintrag() { Cid = 1000, Zaehltag = new DateTime(2015, 1, 1) };

            // when
            MyAssert.Equal(imp1, imp2, 0);

            // then
        }
    }

    public static class MyAssert
    {
        public static void Equal(IEnumerable<ErweiterterGoodSyncLogEintrag> expected,
            IEnumerable<ErweiterterGoodSyncLogEintrag> actual)
        {
            int minLength = Math.Min(expected.Count(), actual.Count());

            for(int i=0; i < minLength; i++)
            {
                var exp = expected.ElementAt(i);
                var act = actual.ElementAt(i);
                MyAssert.Equal(exp, act, i);
            }
        }
    }
}
