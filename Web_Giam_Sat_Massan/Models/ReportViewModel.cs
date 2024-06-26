using Web_Giam_Sat_Massan.Data;

namespace Web_Giam_Sat_Massan.Models
{
    public class ReportViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Shift { get; set; }
        public string Machine { get; set; }
        public List<Record> Records { get; set; } = new List<Record>();
    }
}
