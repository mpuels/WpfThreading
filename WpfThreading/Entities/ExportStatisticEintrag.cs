using System;

namespace WpfThreading.Entities
{
    public class ExportStatisticEintrag
    {
        public int Cid { get; set; }
        public DateTime Zaehltag { get; set; }
        public string Dateiname { get; set; }
        public DateTime? Exportzeitpunkt { get; set; }
    }
}