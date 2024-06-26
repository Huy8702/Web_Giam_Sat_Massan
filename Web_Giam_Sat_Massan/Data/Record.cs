using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;

namespace Web_Giam_Sat_Massan.Data
{
    public class Record
    {
        [Key]
        public int ID { get; set; }
        public DateOnly Date {  get; set; }
        public string Shift { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string ShiftLeaderCode { get; set; }
        public string ShiftLeaderName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Machine { get; set; }
        public int sl {  get; set; }
        public int slt { get; set; }
        public int hs { get; set; }
        public int sgbcgv { get; set; }
        public int ptcgv { get; set; }
        public int sgbr { get; set; }
        public int ptr {  get; set; }
    }
}
