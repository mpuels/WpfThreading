using System.Collections.Generic;
using WpfThreading.Entities;

namespace WpfThreadingTests.Reports
{
    public class ExportStatistic
    {
        private ICollection<ExportStatisticEintrag> eintraege;

        public ExportStatistic()
        {
            eintraege = new List<ExportStatisticEintrag>();
        }

        public ExportStatistic Add(int cid, string zaehltag,
            string dateiname, string exportzeitpunkt)
        {
            eintraege.Add(new ExportStatisticEintrag()
            {
                Cid = cid,
                Zaehltag = Date.ParseExact(zaehltag),
                Dateiname = dateiname,
                Exportzeitpunkt = Date.ParseExact(exportzeitpunkt),
            });
            return this;
        }

        public IEnumerable<ExportStatisticEintrag> Eintraege
        {
            get { return eintraege; }
        }
    }
}
