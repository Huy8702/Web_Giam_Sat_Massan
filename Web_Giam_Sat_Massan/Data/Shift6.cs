using System.ComponentModel.DataAnnotations;

namespace Web_Giam_Sat_Massan.Data
{
    public class Shift6
    {
        [Key]
        public int ID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ShiftLeaderCode { get; set; }
        public string ShiftLeaderName { get; set; } 

    }
}
