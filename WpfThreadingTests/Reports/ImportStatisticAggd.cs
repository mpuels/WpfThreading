using System.Collections.Generic;
using WpfThreading.Entities;

namespace WpfThreadingTests.Reports
{
    public class ImportStatisticAggd
    {
        public List<ImportStatisticAggdEintrag> Eintraege;

        public ImportStatisticAggd()
        {
            Eintraege = new List<ImportStatisticAggdEintrag>();
        }

        public ImportStatisticAggd Add(int cid,
                                   string zaehltag,
                                   string beginDateMin,
                                   string endDateMax,
                                   string importDateMax,
                                   int nDebugTurnusSum,
                                   int nEintraege)
        {
            Eintraege.Add(new ImportStatisticAggdEintrag()
            {
                Cid = cid,
                Zaehltag = Date.ParseExact(zaehltag),
                BeginDateMin = Date.ParseExact(beginDateMin),
                EndDateMax = Date.ParseExact(endDateMax),
                ImportDateMax = Date.ParseExact(importDateMax),
                NDebugTurnusSum = nDebugTurnusSum,
                NEintraege = nEintraege,
            });
            return this;
        }
    }
}
