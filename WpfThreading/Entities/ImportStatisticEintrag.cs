using System;

namespace WpfThreading.Entities
{
    public class ImportStatisticAggdEintrag
    {
        public int Cid { get; set; }
        public DateTime Zaehltag { get; set; }
        public DateTime? BeginDateMin { get; set; }
        public DateTime? EndDateMax { get; set; }
        public DateTime? ImportDateMax { get; set; }
        public int? NDebugTurnusSum { get; set; }
        public int?  NEintraege { get; set; }
    }
}
