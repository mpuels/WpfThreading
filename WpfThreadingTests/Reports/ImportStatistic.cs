using System.Collections.Generic;
using WpfThreading.Entities;

namespace WpfThreadingTests.Reports
{
    public class ImportStatistic
    {
        public List<ImportStatisticEintrag> Eintraege;

        public ImportStatistic()
        {
            Eintraege = new List<ImportStatisticEintrag>();
        }

        public ImportStatistic Add(int cid,
                                   string zaehltag,
                                   int nBinDateien)
        {
            Eintraege.Add(new ImportStatisticEintrag()
            {
                Cid = cid,
                Zaehltag = Date.ParseExact(zaehltag),
                NBinDateien = nBinDateien,
            });
            return this;
        }
    }
}
