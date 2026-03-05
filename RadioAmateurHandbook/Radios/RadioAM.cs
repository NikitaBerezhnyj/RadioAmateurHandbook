using RadioAmateurHandbook.Utils;

namespace RadioAmateurHandbook.Radios
{
    internal class RadioAM : Radio
    {
        private double _minFrequencyLimit = 540.0;
        private double _maxFrequencyLimit = 1600.0;

        public RadioAM() : base()
        {
            Name = "AM Radio";
            Frequency = _minFrequencyLimit;
        }

        public RadioAM(string name) : base(name)
        {
            Frequency = _minFrequencyLimit;
        }

        public RadioAM(string name, bool isPoweredOn, double frequency, int volume, double[] installedFrequency)
            : base(name, isPoweredOn, frequency, volume, installedFrequency)
        {
            if (frequency < _minFrequencyLimit)
            {
                Frequency = _minFrequencyLimit;
            }

            if (frequency > _maxFrequencyLimit)
            {
                Frequency = _maxFrequencyLimit;
            }
        }

        public RadioAM(RadioAM other) : base(other)
        {
            this._minFrequencyLimit = other._minFrequencyLimit;
            this._maxFrequencyLimit = other._maxFrequencyLimit;
        }

        public override void Reset()
        {
            base.Reset();
            Frequency = _minFrequencyLimit;
        }

        public override void SetFrequency(double frequency)
        {
            if (frequency >= _minFrequencyLimit && frequency <= _maxFrequencyLimit)
            {
                Frequency = frequency;
            }
            else
            {
                MessageUtils.PanicMessage("Error: Frequency goes beyond AM waves");
                ConsoleUtils.WaitForEnter();
            }
        }
    }
}
