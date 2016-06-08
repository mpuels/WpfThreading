using System.Collections.Generic;
using WpfThreading.Reports;
using Xunit;

namespace WpfThreadingTests.Reports
{
    public class ZeitstrahlGeneratorTest
    {
        [Fact]
        public void ErstelleZeitstrahlTest()
        {
            // given
            var aktivitaetszeitraeume = new Aktivitaetszeitraeume()
                .Add(1000, "2015-09-30", "2015-10-01")
                .Add(2000, "2015-10-01", "2015-10-03")
                .Eintraege;

            // when
            var actual = ZeitstrahlGenerator.ErstelleZeitstrahl(
                aktivitaetszeitraeume);

            IEnumerable<Zeitstrahleintrag> expected = new Zeitstrahl()
                .Add(1000, "2015-09-30")
                .Add(1000, "2015-10-01")
                .Add(2000, "2015-10-01")
                .Add(2000, "2015-10-02")
                .Add(2000, "2015-10-03")
                .Eintraege;

            // then
            Assert.Equal<Zeitstrahleintrag>(expected, actual);
        }
    }
}
