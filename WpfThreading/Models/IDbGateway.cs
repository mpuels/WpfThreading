using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WpfThreading.Models
{
    public interface IDbGateway
    {
        Task<ICollection<ErweiterterGoodSyncLogEintrag>> FetchErweiterteGoodSyncLogsAsync(ICollection<Aktivitaetszeitraum> aktivitaetszeitraeume, CancellationToken cancellationToken);
    }
}