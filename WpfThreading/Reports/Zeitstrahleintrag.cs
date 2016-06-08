using System;

namespace WpfThreading.Reports
{
    public class Zeitstrahleintrag : IEquatable<Zeitstrahleintrag>
    {
        public int Cid { get; set; }
        public DateTime Zaehltag { get; set; }

        public bool Equals(Zeitstrahleintrag other)
        {
            if (other == null)
            {
                return false;
            }
            return Cid == other.Cid
                && Zaehltag == other.Zaehltag;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Zeitstrahleintrag zeitstrahleintragObj =
                obj as Zeitstrahleintrag;

            if(zeitstrahleintragObj == null)
            {
                return false;
            }
            else
            {
                return Equals(zeitstrahleintragObj);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}