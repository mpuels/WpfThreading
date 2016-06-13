using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WpfThreading.Entities;

namespace WpfThreading.Services
{
    public interface IDbGateway
    {
        Task<IEnumerable<ImportStatisticAggdEintrag>> Public_BinaryData_Import_Statistic_Aggd_Async(
            IEnumerable<Aktivitaetszeitraum> aktivitaetszeitraeume,
            CancellationToken cancellationToken);

        Task<IEnumerable<ExportStatisticEintrag>> Public_BinaryData_Export_Statistic_Async(
            IEnumerable<Aktivitaetszeitraum> aktivitaetszeitraeume,
            CancellationToken cancellationToken);

        Task<IEnumerable<Entities.pgmssql.BinaryData_Controller>> Pgmssql_BinaryData_ControllerAsync(
            IEnumerable<int> cids,
            CancellationToken cancellationToken);
    }
}