using System;

namespace WpfThreading.Entities
{
    /// <summary>
    /// Repräsentiert einen Eintrag für die erweiterten GoodSync-Logs
    /// 
    /// Bedeutung der Präfixe der Properties:
    /// 
    /// o IS: BinaryData_Import_Statistic
    /// o EF: BinaryData_Export_Files
    /// o ES: BinaryData_Export_Statistic
    /// o GL: GoodSync-Logs
    /// </summary>
    public class ErweiterterGoodSyncLogEintrag : IEquatable<ErweiterterGoodSyncLogEintrag>
    {
        public int Cid { get; set; }
        public DateTime Zaehltag { get; set; }

        // Infos aus Import-Statistik.
        public DateTime? ISBeginDateMin { get; set; }
        public DateTime? ISEndDateMax { get; set; }
        public DateTime? ISImportDateMax { get; set; }
        public int? ISNDebugTurnusSum { get; set; }
        public int? ISNEintraege { get; set; }

        // Infos aus Export-Statistik.
        public DateTime? ESBeginDate { get; set; }
        public DateTime? ESEndDate { get; set; }
        public DateTime? ESExportDate { get; set; }
        public int? ESFileIdx { get; set; }

        public bool Equals(ErweiterterGoodSyncLogEintrag other)
        {
            if (other == null)
            {
                return false;
            }
            else
            {
                return Cid == other.Cid
                    && Zaehltag == other.Zaehltag

                    // Import-Statistik.
                    && ISBeginDateMin == other.ISBeginDateMin
                    && ISEndDateMax == other.ISEndDateMax
                    && ISImportDateMax == other.ISImportDateMax
                    && ISNDebugTurnusSum == other.ISNDebugTurnusSum
                    && ISNEintraege == other.ISNEintraege
                    
                    // Export-Statistik.
                    && ESBeginDate == other.ESBeginDate
                    && ESEndDate == other.ESEndDate
                    && ESExportDate == other.ESExportDate
                    && ESFileIdx == other.ESFileIdx
                    ;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            ErweiterterGoodSyncLogEintrag objTyped =
                obj as ErweiterterGoodSyncLogEintrag;

            if (objTyped == null)
            {
                return false;
            }
            else
            {
                return Equals(objTyped);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
