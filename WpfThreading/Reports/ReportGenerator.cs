using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WpfThreading.Entities;
using WpfThreading.Services;

namespace WpfThreading.Reports
{
    public class ReportGenerator : IReportGenerator
    {
        private IDbGateway dbGateway;

        public ReportGenerator(IDbGateway dbGateway)
        {
            this.dbGateway = dbGateway;
        }

        public async Task<IEnumerable<ErweiterterGoodSyncLogEintrag>> ErweiterteGoodSyncLogsAsync(
            IEnumerable<Aktivitaetszeitraum> aktivitaetszeitraeume,
            CancellationToken cancellationToken)
        {
            var zeitstrahl = ZeitstrahlGenerator.ErstelleZeitstrahl(
                aktivitaetszeitraeume);

            var impstat = await dbGateway.Public_BinaryData_Import_Statistic_Aggd_Async(
                aktivitaetszeitraeume, cancellationToken);

            var expstat = await dbGateway.Public_BinaryData_Export_Statistic_Async(
                aktivitaetszeitraeume, cancellationToken);

            var erweiterteGoodSyncLogs =
                (from zs in zeitstrahl
                 join imps in impstat
                     on new { cid = zs.Cid, zaehltag = zs.Zaehltag }
                         equals new { cid = imps.Cid, zaehltag = imps.Zaehltag }
                     into imps_
                     from imps__ in imps_.DefaultIfEmpty(new ImportStatisticAggdEintrag())
                 join exps in expstat
                     on new { cid = zs.Cid, zaehltag = zs.Zaehltag }
                         equals new { cid = exps.Cid, zaehltag = exps.Zaehltag }
                     into exps_
                     from exps__ in exps_.DefaultIfEmpty(new ExportStatisticEintrag())
                 select new ErweiterterGoodSyncLogEintrag()
                 {
                     Cid = zs.Cid,
                     Zaehltag = zs.Zaehltag,

                     // Import-Statistik.
                     ISBeginDateMin = imps__.BeginDateMin,
                     ISEndDateMax = imps__.EndDateMax,
                     ISImportDateMax = imps__.ImportDateMax,
                     ISNDebugTurnusSum = imps__.NDebugTurnusSum,
                     ISNEintraege = imps__.NEintraege,

                     // Export-Statistik.
                     ESBeginDate = exps__.BeginDate,
                     ESEndDate = exps__.EndDate,
                     ESExportDate = exps__.ExportDate,
                     ESFileIdx = exps__.FileIdx,
                 }
                 ).ToList();

            return erweiterteGoodSyncLogs;
        }
    }
}
