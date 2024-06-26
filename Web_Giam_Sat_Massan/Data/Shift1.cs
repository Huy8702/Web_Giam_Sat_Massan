using System.ComponentModel.DataAnnotations;

namespace Web_Giam_Sat_Massan.Data
{
    public class Shift1
    {
        [Key]
        public int ID { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string ShiftLeaderCode { get; set; }
        public string ShiftLeaderName { get; set; }
        
    }
}
