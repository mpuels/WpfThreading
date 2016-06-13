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

                                          // Import-Statistik.
                                          string isBeginDateMin,
                                          string isEndDateMax,
                                          string isImportDateMax,
                                          int? isNDebugTurnusSum,
                                          int? isNEintraege,
                                          
                                          // Export-Statistik.
                                          string esBeginDate,
                                          string esEndDate,
                                          string esExportDate,
                                          int? esFileIdx)
        {
            Eintraege.Add(new ErweiterterGoodSyncLogEintrag()
            {
                Cid = cid,
                Zaehltag = Date.ParseExact(zaehltag),

                // Import-Statistik.
                ISBeginDateMin = Date.ParseIfNotNull(isBeginDateMin),
                ISEndDateMax = Date.ParseIfNotNull(isEndDateMax),
                ISImportDateMax = Date.ParseIfNotNull(isImportDateMax),
                ISNDebugTurnusSum = isNDebugTurnusSum,
                ISNEintraege = isNEintraege,

                // Export-Statistik.
                ESBeginDate = Date.ParseIfNotNull(esBeginDate),
                ESEndDate = Date.ParseIfNotNull(esEndDate),
                ESExportDate = Date.ParseIfNotNull(esExportDate),
                ESFileIdx = esFileIdx,
            });

            return this;
        }
    }
}
