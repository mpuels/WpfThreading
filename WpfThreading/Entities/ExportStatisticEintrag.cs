using System;

namespace WpfThreading.Entities
{
    public class ExportStatisticEintrag
    {
        public int Cid { get; set; }
        public DateTime Zaehltag { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ExportDate { get; set; }
        public int? FileIdx { get; set; }
    }
}