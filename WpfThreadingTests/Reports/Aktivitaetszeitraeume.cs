using System.Collections.Generic;
using WpfThreading.Entities;

namespace WpfThreadingTests.Reports
{
    /// <summary>
    /// Stellt eine veränderliche Sammlung von Aktivitätszeiträumen dar.
    /// 
    /// Mit dieser Klasse kann eine Sammlung von Aktivitätszeiträumen
    /// kurz und knapp für Tests erstellt werden.
    /// </summary>
    public class Aktivitaetszeitraeume
    {
        private ICollection<Aktivitaetszeitraum> eintraege;

        public Aktivitaetszeitraeume()
        {
            eintraege = new List<Aktivitaetszeitraum>();
        }

        public ICollection<Aktivitaetszeitraum> Eintraege
        {
            get { return eintraege; }
        }

        public Aktivitaetszeitraeume Add(int cid, string von, string bis)
        {
            eintraege.Add(new Aktivitaetszeitraum()
            {
                Cid = cid,
                Von = Date.ParseExact(von),
                Bis = Date.ParseExact(bis),
            });
            return this;
        }
    }
}
