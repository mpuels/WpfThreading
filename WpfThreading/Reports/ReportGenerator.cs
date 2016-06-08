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
            var impstat = await dbGateway.FetchImportStatisticAsync(
                aktivitaetszeitraeume, cancellationToken);

            var expstat = await dbGateway.FetchExportStatisticAsync(
                aktivitaetszeitraeume, cancellationToken);

            var zeitstrahl = ZeitstrahlGenerator.ErstelleZeitstrahl(
                aktivitaetszeitraeume);

            var erweiterteGoodSyncLogs =
                (from zs in zeitstrahl
                 join imps in impstat
                     on new { cid = zs.Cid, zaehltag = zs.Zaehltag }
                         equals new { cid = imps.Cid, zaehltag = imps.Zaehltag }
                     into imps_
                     from imps__ in imps_.DefaultIfEmpty(new ImportStatisticEintrag())
                 join exps in expstat
                     on new { cid = zs.Cid, zaehltag = zs.Zaehltag }
                         equals new { cid = exps.Cid, zaehltag = exps.Zaehltag }
                     into exps_
                     from exps__ in exps_.DefaultIfEmpty(new ExportStatisticEintrag())
                 select new ErweiterterGoodSyncLogEintrag()
                 {
                     Cid = zs.Cid,
                     Zaehltag = zs.Zaehltag,
                     Dateiname = exps__.Dateiname,
                     ISNBinDateien = imps__.NBinDateien,
                     ESExportZeitpunkt = exps__.Exportzeitpunkt,
                 }
                 ).ToList();

            return erweiterteGoodSyncLogs;
        }
    }
}
