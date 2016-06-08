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

            IEnumerable<ImportStatisticEintrag> impstat =
                new ImportStatistic()
                .Add(1000, "2015-10-01", 1)
                .Add(1000, "2015-10-02", 2)
                .Add(2000, "2015-10-03", 4)
                .Eintraege;

            IEnumerable<ExportStatisticEintrag> expstat =
                new ExportStatistic()
                .Add(1000, "2015-10-01", "052_1000_file1.txt", "2015-10-03 08:00:00")
                .Add(2000, "2015-10-03", "052_2000_file1.txt", "2015-10-05 08:00:01")
                .Eintraege;

            IEnumerable<ErweiterterGoodSyncLogEintrag> expected =
                new ErweiterteGoodSyncLogs()
                .Add(1000, "2015-10-01", "052_1000_file1.txt",    1, "2015-10-03 08:00:00")
                .Add(1000, "2015-10-02",                 null,    2,                  null)
                .Add(2000, "2015-10-02",                 null, null,                  null)
                .Add(2000, "2015-10-03", "052_2000_file1.txt",    4, "2015-10-05 08:00:01")
                .Eintraege;

            var dbGatewayMock = new Mock<IDbGateway>();

            dbGatewayMock
                .Setup(g => g.FetchImportStatisticAsync(aktivitaetszeitraeume, cancellationTokenStub))
                .ReturnsAsync(impstat);

            dbGatewayMock
                .Setup(g => g.FetchExportStatisticAsync(aktivitaetszeitraeume, cancellationTokenStub))
                .ReturnsAsync(expstat);

            var sut = new ReportGenerator(dbGatewayMock.Object);

            // when
            var t = sut.ErweiterteGoodSyncLogsAsync(
                aktivitaetszeitraeume, cancellationTokenStub);
            //t.RunSynchronously();
            var actual = t.Result;

            // then
            Assert.Equal<ErweiterterGoodSyncLogEintrag>(expected, actual);
        }
    }
}
