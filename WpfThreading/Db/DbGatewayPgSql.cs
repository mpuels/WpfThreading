using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WpfThreading.Entities;
using WpfThreading.Services;

namespace WpfThreading.Db
{
    public class DbGatewayPgSql : IDbGateway
    {
        private string connectionString;

        public DbGatewayPgSql(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<ExportStatisticEintrag>> Public_BinaryData_Export_Statistic_Async(
            IEnumerable<Aktivitaetszeitraum> aktivitaetszeitraeume,
            CancellationToken cancellationToken)
        {
            var exportstatistic = new List<ExportStatisticEintrag>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var selectCmd = new NpgsqlCommand())
                {
                    selectCmd.Connection = conn;
                    selectCmd.CommandText = @"
                        SELECT
                            t.""BinaryData_ControllerId"" AS cid,
                            t.""BinaryData_BeginDate""::date AS zaehltag,
                            t.""BinaryData_BeginDate"" AS begin_date,
                            t.""BinaryData_EndDate"" AS end_date,
                            t.""BinaryData_ExportDate"" AS export_date,
                            t.""BinaryData_FileIdx"" AS file_idx
                        FROM
                            public.""BinaryData_Export_Statistic"" as t
                        WHERE
                            t.cid = @cid AND
                            t.""BinaryData_BeginDate"" >= @von AND
                            t.""BinaryData_BeginDate""  < @bis + '1day'
                    ";

                    foreach (var aktivitaetszeitraum in aktivitaetszeitraeume)
                    {
                        selectCmd.Parameters.Clear();
                        selectCmd.Parameters.AddWithValue("@cid", aktivitaetszeitraum.Cid);
                        selectCmd.Parameters.AddWithValue("@von", aktivitaetszeitraum.Von);
                        selectCmd.Parameters.AddWithValue("@bis", aktivitaetszeitraum.Bis);

                        var reader = await selectCmd.ExecuteReaderAsync(cancellationToken);

                        while (reader.Read())
                        {
                            exportstatistic.Add(new ExportStatisticEintrag()
                            {
                                Cid = reader.GetInt32(0),
                                Zaehltag = reader.GetDateTime(1),
                                BeginDate = reader.GetDateTime(2),
                                EndDate = reader.GetDateTime(3),
                                ExportDate = reader.GetDateTime(4),
                                FileIdx = reader.GetInt32(5),
                            });
                        }
                        reader.Close();
                    }
                }
            }
            return exportstatistic;
        }

        public async Task<IEnumerable<ImportStatisticAggdEintrag>> Public_BinaryData_Import_Statistic_Aggd_Async(
            IEnumerable<Aktivitaetszeitraum> aktivitaetszeitraeume,
            CancellationToken cancellationToken)
        {
            var importstatistic = new List<ImportStatisticAggdEintrag>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var selectCmd = new NpgsqlCommand())
                {
                    selectCmd.Connection = conn;
                    selectCmd.CommandText = @"
                        SELECT
                            t.""BinaryData_ControllerId"" AS cid,
                            t.""BinaryData_BeginDate""::date AS zaehltag,
                            MIN(t.""BinaryData_BeginDate"") AS begin_date_min,
                            MAX(t.""BinaryData_EndDate"") AS end_date_max,
                            MAX(t.""BinaryData_ImportDate"") AS import_date_max,
                            SUM(t.""BinaryData_DataCount_DebugTurnus"") AS n_debug_turnus_sum,
                            COUNT(*) AS n_eintraege,
                        FROM
                            public.""BinaryData_Import_Statistic"" as t
                        WHERE
                            t.""BinaryData_ControllerId"" = @cid AND
                            t.""BinaryData_BeginDate"" >= @von AND
                            t.""BinaryData_BeginDate""  < @bis + '1day'
                        GROUP BY
                            t.""BinaryData_ControllerId"",
                            t.""BinaryData_BeginDate""::date
                        ORDER BY
                            t.""BinaryData_ControllerId"",
                            t.""BinaryData_BeginDate""::date
                    ";

                    foreach (var aktivitaetszeitraum in aktivitaetszeitraeume)
                    {
                        selectCmd.Parameters.Clear();
                        selectCmd.Parameters.AddWithValue("@cid", aktivitaetszeitraum.Cid);
                        selectCmd.Parameters.AddWithValue("@von", aktivitaetszeitraum.Von);
                        selectCmd.Parameters.AddWithValue("@bis", aktivitaetszeitraum.Bis);

                        var reader = await selectCmd.ExecuteReaderAsync(cancellationToken);

                        while (reader.Read())
                        {
                            importstatistic.Add(new ImportStatisticAggdEintrag()
                            {
                                Cid = reader.GetInt32(0),
                                Zaehltag = reader.GetDateTime(1),
                                BeginDateMin = reader.GetDateTime(2),
                                EndDateMax = reader.GetDateTime(3),
                                ImportDateMax = reader.GetDateTime(4),
                                NDebugTurnusSum = reader.GetInt32(5),
                                NEintraege = reader.GetInt32(6),
                            });
                        }
                        reader.Close();
                    }
                }
            }
            return importstatistic;
        }

        public async Task<IEnumerable<Entities.pgmssql.BinaryData_Controller>> Pgmssql_BinaryData_ControllerAsync(
            IEnumerable<int> cids,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
