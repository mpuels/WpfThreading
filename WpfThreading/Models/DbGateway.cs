using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WpfThreading.Models
{
    public class DbGateway : IDbGateway
    {
        public async Task<ICollection<ErweiterterGoodSyncLogEintrag>> FetchErweiterteGoodSyncLogsAsync(ICollection<Aktivitaetszeitraum> aktivitaetszeitraeume,
                                                                                                       CancellationToken cancellationToken)
        {
            var t = new Task<ICollection<ErweiterterGoodSyncLogEintrag>>(() => FetchErweiterteGoodSyncLogs(aktivitaetszeitraeume),
                                                                         cancellationToken);
            var delayTask = Task.Delay(5000, cancellationToken);
            t.Start();
            var erweiterteGoodSyncLogs = await t;
            await delayTask;
            return erweiterteGoodSyncLogs;
        }

        private ICollection<ErweiterterGoodSyncLogEintrag> FetchErweiterteGoodSyncLogs(ICollection<Aktivitaetszeitraum> aktivitaetszeitraeume)
        {
            var erweiterteGoodSyncLogs = new List<ErweiterterGoodSyncLogEintrag>();

            erweiterteGoodSyncLogs.Add(new ErweiterterGoodSyncLogEintrag
            {
                Cid = 1000,
                Zaehltag = new DateTime(2016, 5, 1)
            });

            erweiterteGoodSyncLogs.Add(new ErweiterterGoodSyncLogEintrag
            {
                Cid = 1000,
                Zaehltag = new DateTime(2016, 5, 2)
            });

            return erweiterteGoodSyncLogs;
        }
    }
}
