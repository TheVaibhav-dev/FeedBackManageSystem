using Microsoft.VisualBasic;

namespace FeedBackManageSystem.HelperClasses
{
    public static class Encryption
    {
        /// <summary>
        /// Created By : Vaibhav Srivastava
        /// Created Date : 26 Dec 2025
        /// Description : To fetch all feedback entries from the database and display them in the view.
        /// </summary>
        /// <returns></returns>
        public static string EncyptNumber(Int64 theNumber)
        {
            string szEnc = null;
            string nn = string.Empty;
            double n = Math.Pow((theNumber + 1570), 2) - 7 * (theNumber + 1570) - 450;
            if (n < 0)
                szEnc = "R";
            else
                szEnc = "J";
            nn = Math.Abs(n).ToString();
            for (int i = 1; i <= nn.Length; i += 2)
            {
                string t = Strings.Mid(nn, i, 2);
                if (t.Length == 1)
                {
                    szEnc = szEnc + t;
                    break; // TODO: might not be correct. Was : Exit For
                }
                int HiN = (Convert.ToInt32(t) & 240) / 16;
                int LoN = Convert.ToInt32(t) & 15;
                szEnc = szEnc + Strings.Chr(Strings.Asc("M") + HiN) + Strings.Chr(Strings.Asc("C") + LoN);
            }
            return szEnc;
        }
        /// <summary>
        /// Created By : Vaibhav Srivastava
        /// Created Date : 26 Dec 2025
        /// Description : To fetch all feedback entries from the database and display them in the view.
        /// </summary>
        /// <returns></returns>
        public static Int64 DecryptNumber(string theNumber)
        {
            // ERROR: Not supported in C#: OnErrorStatement

            string e = null;
            int sign;
            string t = null;
            string HiN = null;
            string LoN = null;
            string NewN = null;
            //object i = null;
            e = theNumber;
            if (Strings.Left(e, 1) == "R")
                sign = -1;
            else
                sign = 1;
            e = Strings.Mid(e, 2);
            NewN = "";
            for (int i = 1; i <= Strings.Len(e); i += 2)
            {
                t = Strings.Mid(e, i, 2);
                if (Strings.Asc(t) >= Strings.Asc("0") & Strings.Asc(t) <= Strings.Asc("9"))
                {
                    NewN = NewN + t;
                    break; // TODO: might not be correct. Was : Exit For
                }
                HiN = Strings.Mid(t, 1, 1);
                LoN = Strings.Mid(t, 2, 1);
                int HiN_N = (Strings.Asc(HiN) - Strings.Asc("M")) * 16;
                int LoN_N = Strings.Asc(LoN) - Strings.Asc("C");
                t = Convert.ToString(HiN_N + LoN_N);
                if (Strings.Len(t) == 1)
                    t = "0" + t;
                NewN = NewN + t;
            }
            double ee = Convert.ToDouble(NewN) * sign;
            return Convert.ToInt64((7 + Math.Sqrt(49 - 4 * (-450 - ee))) / 2 - 1570);
        }
    }
}
