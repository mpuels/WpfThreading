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
        public string Dateiname { get; set; }
        public int? ISNBinDateien { get; set; }
        public DateTime? ESExportZeitpunkt { get; set; }

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
                    && Dateiname == other.Dateiname
                    && ISNBinDateien == other.ISNBinDateien
                    && ESExportZeitpunkt == other.ESExportZeitpunkt;
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
