using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfThreading.Reports;

namespace WpfThreadingTests.Reports
{
    public class Zeitstrahl
    {
        public List<Zeitstrahleintrag> Eintraege;

        public Zeitstrahl()
        {
            Eintraege = new List<Zeitstrahleintrag>();
        }

        public Zeitstrahl Add(int cid, string zaehltag)
        {
            Eintraege.Add(new Zeitstrahleintrag()
            {
                Cid = cid,
                Zaehltag = Date.ParseExact(zaehltag),
            });
            return this;
        }
    }
}
