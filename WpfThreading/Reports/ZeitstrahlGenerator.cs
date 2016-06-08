using System.Collections.Generic;
using System.Linq;
using WpfThreading.Entities;

namespace WpfThreading.Reports
{
    public static class ZeitstrahlGenerator
    {
        public static IEnumerable<Zeitstrahleintrag> ErstelleZeitstrahl(
            IEnumerable<Aktivitaetszeitraum> aktivitaetszeitraeume)
        {
            var zeitstrahl = new List<Zeitstrahleintrag>();

            foreach (var aktivitaetszeitraum in aktivitaetszeitraeume)
            {
                int nTage = aktivitaetszeitraum.Bis
                            .Subtract(aktivitaetszeitraum.Von).Days
                            + 1;
                var z = Enumerable.Range(0, nTage)
                                  .Select(t => new Zeitstrahleintrag()
                                  {
                                      Cid = aktivitaetszeitraum.Cid,
                                      Zaehltag = aktivitaetszeitraum.Von.AddDays(t)
                                  });

                zeitstrahl.AddRange(z);
            }
            return zeitstrahl;
        }
    }
}
