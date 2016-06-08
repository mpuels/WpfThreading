using System.Collections.Generic;
using WpfThreading.Entities;

namespace WpfThreadingTests.Reports
{
    /// <summary>
    /// Stellt eine veränderliche Sammlung von erweiterten GoodSync-Log-Einträgen dar.
    /// 
    /// Mit Hilfe dieser Klasse können erweiterte GoodSync-Logs
    /// kurz und knapp für Tests erstellt werden.
    /// </summary>
    public class ErweiterteGoodSyncLogs
    {
        public List<ErweiterterGoodSyncLogEintrag> Eintraege;

        public ErweiterteGoodSyncLogs()
        {
            Eintraege = new List<ErweiterterGoodSyncLogEintrag>();
        }

        public ErweiterteGoodSyncLogs Add(int cid,
                                          string zaehltag,
                                          string dateiname,
                                          int? isNBinDateien,
                                          string esExportzeitpunkt)
        {
            Eintraege.Add(new ErweiterterGoodSyncLogEintrag()
            {
                Cid = cid,
                Zaehltag = Date.ParseExact(zaehltag),
                Dateiname = dateiname,
                ISNBinDateien = isNBinDateien,
                ESExportZeitpunkt = Date.ParseIfNotNull(esExportzeitpunkt),
            });

            return this;
        }
    }
}
