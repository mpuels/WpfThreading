using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WpfThreading.Entities;
using WpfThreading.Services;

namespace WpfThreading.Db
{
    /// <summary>
    /// DbGateway für Demozwecke.
    /// 
    /// Das Gateway greift nicht auf eine externe Datenbank zu,
    /// sondern gibt immer dieselben Daten zurück.
    /// Die Rückgabe der Daten wird künstlich verzögert.
    /// </summary>
    public class DbGatewayDemo : IDbGateway
    {
        public async Task<IEnumerable<ImportStatisticAggdEintrag>> Public_BinaryData_Import_Statistic_Aggd_Async(
            IEnumerable<Aktivitaetszeitraum> aktivitaetszeitraeume,
            CancellationToken cancellationToken)
        {
            var t = new Task<ICollection<ImportStatisticAggdEintrag>>(
                () => importstatisticDummies(aktivitaetszeitraeume),
                cancellationToken);

            var delayTask = Task.Delay(5000, cancellationToken);
            t.Start();
            var erweiterteGoodSyncLogs = await t;
            await delayTask;
            return erweiterteGoodSyncLogs;
        }

        public async Task<IEnumerable<ExportStatisticEintrag>> Public_BinaryData_Export_Statistic_Async(
            IEnumerable<Aktivitaetszeitraum> aktivitaetszeitraeume,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Entities.pgmssql.BinaryData_Controller>> Pgmssql_BinaryData_ControllerAsync(
            IEnumerable<int> cids,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private ICollection<ImportStatisticAggdEintrag> importstatisticDummies(
            IEnumerable<Aktivitaetszeitraum> aktivitaetszeitraeume)
        {
            var erweiterteGoodSyncLogs = new List<ImportStatisticAggdEintrag>();

            erweiterteGoodSyncLogs.Add(new ImportStatisticAggdEintrag
            {
                Cid = 1000,
                Zaehltag = new DateTime(2016, 5, 1)
            });

            erweiterteGoodSyncLogs.Add(new ImportStatisticAggdEintrag
            {
                Cid = 1000,
                Zaehltag = new DateTime(2016, 5, 2)
            });

            return erweiterteGoodSyncLogs;
        }
    }
}
