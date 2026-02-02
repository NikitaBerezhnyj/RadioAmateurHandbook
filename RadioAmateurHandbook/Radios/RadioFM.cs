using RadioAmateurHandbook.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.Radios
{
    internal class RadioFM: Radio
    {
        private double _minFrequencyLimit = 87.5;
        private double _maxFrequencyLimit = 108.0;

        public RadioFM(): base() {
            this.name = "FM Radio";
            this.frequency = _minFrequencyLimit;
        }
        public RadioFM(string name) : base(name)
        {
            this.frequency = _minFrequencyLimit;
        }
        public RadioFM(string name, bool isPoweredOn, double frequency, int volume, double[] installedFrequency)
        : base(name, isPoweredOn, frequency, volume, installedFrequency)
        {
            if (frequency < _minFrequencyLimit) this.frequency = _minFrequencyLimit;
            if (frequency > _maxFrequencyLimit) this.frequency = _maxFrequencyLimit;
        }
        public RadioFM(RadioFM other): base(other) {
            this._minFrequencyLimit = other._minFrequencyLimit;
            this._maxFrequencyLimit = other._maxFrequencyLimit;
        }

        public override void Reset()
        {
            base.Reset();
            frequency = _minFrequencyLimit;
        }

        public override void SetFrequency(double frequency) {
            if (frequency >= _minFrequencyLimit && frequency <= _maxFrequencyLimit)
            {
                this.frequency = frequency;
            }
            else
            {
                MessageUtils.PanicMessage("Error: Frequency goes beyond FM waves");
                ConsoleUtils.WaitForEnter();
            }
        }
    }
}
