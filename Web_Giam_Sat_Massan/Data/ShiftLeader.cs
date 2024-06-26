using System.ComponentModel.DataAnnotations;

namespace Web_Giam_Sat_Massan.Data
{
    public class ShiftLeader
    {
        [Key]
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
