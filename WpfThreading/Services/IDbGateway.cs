using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WpfThreading.Entities;

namespace WpfThreading.Services
{
    public interface IDbGateway
    {
        Task<IEnumerable<ImportStatisticEintrag>> FetchImportStatisticAsync(
            IEnumerable<Aktivitaetszeitraum> aktivitaetszeitraeume,
            CancellationToken cancellationToken);

        Task<IEnumerable<ExportStatisticEintrag>> FetchExportStatisticAsync(
            IEnumerable<Aktivitaetszeitraum> aktivitaetszeitraeume,
            CancellationToken cancellationToken);
    }
}