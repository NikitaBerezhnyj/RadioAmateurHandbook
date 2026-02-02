using RadioAmateurHandbook.Data.DTO;
using RadioAmateurHandbook.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.Radios
{
    internal abstract class Radio
    {
        private int _maxVolume = 120;

        protected string name;
        protected bool isPoweredOn;
        protected double frequency;
        protected int volume;
        protected double[] installedFrequency = new double[5];

        public Radio() {
            this.name = "Radio";
            this.isPoweredOn = false;
            this.frequency = 100.0;
            this.volume = 10;
			for (int i = 0; i < installedFrequency.Length; i++)
				installedFrequency[i] = 0;
		}
        public Radio(string name)
        {
            this.name = name;
            this.isPoweredOn = false;
            this.frequency = 100.0;
            this.volume = 10;
            this.installedFrequency = [];
        }
        public Radio(string name, bool isPoweredOn, double frequency, int volume, double[] installedFrequency) {
            this.name = name;
            this.isPoweredOn = isPoweredOn;
            this.frequency = frequency;
            this.volume = volume;
            this.installedFrequency = installedFrequency;
        }
        public Radio(Radio other) {
            this.name = other.name;
            this.isPoweredOn = other.isPoweredOn;
            this.frequency = other.frequency;
            this.volume = other.volume;
            this.installedFrequency = other.installedFrequency;
        }

		public virtual void Reset()
		{
			isPoweredOn = false;
			volume = 10;
			frequency = 100.0;
			for (int i = 0; i < installedFrequency.Length; i++)
				installedFrequency[i] = 0;
		}

		public void Load(RadioDTO dto)
        {
            this.name = dto.Name;
            this.isPoweredOn = dto.IsPoweredOn;
            this.frequency = dto.Frequency;
            this.volume = dto.Volume;
            this.installedFrequency = dto.InstalledFrequencies;
        }

        public string GetName() { return name; }

        public void TurnOn() { this.isPoweredOn = true; }
        public void TurnOff() { this.isPoweredOn = false;  }
        public bool IsPoweredOn() { return this.isPoweredOn; }

        public double GetFrequency() { return this.frequency; }
        public abstract void SetFrequency(double frequency);

        public int GetIVolume() { return this.volume; }
        public void SetVolume(int volume) {
            if (volume <= 0)
            {
                volume = 0;
            }
            else if (volume > _maxVolume)
            {
                MessageUtils.PanicMessage("Error: Volume is higher than maximum");
                ConsoleUtils.WaitForEnter();
                return;
            }

            this.volume = volume;
        }

        public double[] GetInstalledFrequency() { return this.installedFrequency; }
        public void SaveFrequency(int index, double newFrequency) {
            if (index >= 0 && index < this.installedFrequency.Length)
            {
                this.installedFrequency[index] = newFrequency;
            }
            else
            {
                MessageUtils.PanicMessage("Error: Invalid index");
                ConsoleUtils.WaitForEnter();
                return;
            }
        }
        public void LoadFrequency(int index) {
            if (index < 1 || index > this.installedFrequency.Length)
            {
                MessageUtils.PanicMessage("Error: Invalid index");
                ConsoleUtils.WaitForEnter();
                return;
            }

            double value = this.installedFrequency[index - 1];
            if (value == 0)
            {
                MessageUtils.PanicMessage("Error: Value by index is empty");
                ConsoleUtils.WaitForEnter();
                return;
            }

            SetFrequency(value);
        }

    }
}
