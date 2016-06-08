using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WpfThreading.Entities;

namespace WpfThreading.Services
{
    public interface IReportGenerator
    {
        Task<IEnumerable<ErweiterterGoodSyncLogEintrag>> ErweiterteGoodSyncLogsAsync(
            IEnumerable<Aktivitaetszeitraum> aktivitaetszeitraeume,
            CancellationToken cancellationToken);
    }
}