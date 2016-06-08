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

        public async Task<IEnumerable<ExportStatisticEintrag>> FetchExportStatisticAsync(
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
                            t.cid,
                            t.zaehltag,
                            t.dateiname,
                            t.exportzeitpunkt
                        FROM
                            exportstatistic as t
                        WHERE
                            t.cid = @cid AND
                            t.zaehltag >= @von AND
                            t.zaehltag <= @bis
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
                                Dateiname = reader.GetString(2),
                                Exportzeitpunkt = reader.GetDateTime(3),
                            });
                        }
                        reader.Close();
                    }
                }
            }
            return exportstatistic;
        }

        public async Task<IEnumerable<ImportStatisticEintrag>> FetchImportStatisticAsync(
            IEnumerable<Aktivitaetszeitraum> aktivitaetszeitraeume,
            CancellationToken cancellationToken)
        {
            var importstatistic = new List<ImportStatisticEintrag>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var selectCmd = new NpgsqlCommand())
                {
                    selectCmd.Connection = conn;
                    selectCmd.CommandText = @"
                        SELECT
                            t.cid,
                            t.begin_date::date::timestamp AS zaehltag,
                            COUNT(*) AS n_bindateien
                        FROM
                            importstatistic as t
                        WHERE
                            t.cid = @cid AND
                            t.begin_date >= @von AND
                            t.begin_date <= @bis
                        GROUP BY
                            t.cid,
                            t.begin_date::date::timestamp
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
                            importstatistic.Add(new ImportStatisticEintrag()
                            {
                                Cid = reader.GetInt32(0),
                                Zaehltag = reader.GetDateTime(1),
                                NBinDateien = reader.GetInt32(2),
                            });
                        }
                        reader.Close();
                    }
                }
            }
            return importstatistic;
        }
    }
}
