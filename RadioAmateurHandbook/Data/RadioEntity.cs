using System.ComponentModel.DataAnnotations;

namespace RadioAmateurHandbook.Data
{
    public class RadioEntity
    {
        [Key]
        public string Type { get; set; }

        public string Name { get; set; }
        public bool IsPoweredOn { get; set; }
        public double Frequency { get; set; }
        public int Volume { get; set; }

        public string InstalledFrequenciesJson { get; set; }
    }
}
