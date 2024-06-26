using Web_Giam_Sat_Massan.Data;

namespace Web_Giam_Sat_Massan.Models
{
    public class HomeViewModel
    {
        public PlcData plcData { get; set; }
        public Shift1 s1 { get; set; }
        public Shift2 s2 { get; set; }
        public Shift3 s3 { get; set; }
        public MSProduct mSProduct { get; set; }
        public bool isconnect { get; set; }

    }
}
