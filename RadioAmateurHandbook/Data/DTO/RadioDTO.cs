using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.Data.DTO
{
    internal class RadioDTO
    {
        public string Name { get; set; }
        public bool IsPoweredOn { get; set; }
        public double Frequency { get; set; }
        public int Volume { get; set; }
        public double[] InstalledFrequencies { get; set; }

        public RadioDTO() { }

        public RadioDTO(Radios.Radio radio)
        {
            Name = radio.GetName();
            IsPoweredOn = radio.IsPoweredOn();
            Frequency = radio.GetFrequency();
            Volume = radio.GetIVolume();
            InstalledFrequencies = radio.GetInstalledFrequency();
        }
    }

    internal class RadioData
    {
        public RadioDTO FMRadio { get; set; }
        public RadioDTO AMRadio { get; set; }
    }
}
