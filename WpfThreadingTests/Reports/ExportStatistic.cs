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
            string beginDate, string endDate, string exportDate, int fileIdx)
        {
            eintraege.Add(new ExportStatisticEintrag()
            {
                Cid = cid,
                Zaehltag = Date.ParseExact(zaehltag),
                BeginDate = Date.ParseExact(beginDate),
                EndDate = Date.ParseExact(endDate),
                ExportDate = Date.ParseExact(exportDate),
                FileIdx = fileIdx,
            });
            return this;
        }

        public IEnumerable<ExportStatisticEintrag> Eintraege
        {
            get { return eintraege; }
        }
    }
}
