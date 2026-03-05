using RadioAmateurHandbook.Utils;

namespace RadioAmateurHandbook.Radios
{
    internal class RadioFM : Radio
    {
        private double _minFrequencyLimit = 87.5;
        private double _maxFrequencyLimit = 108.0;

        public RadioFM() : base()
        {
            Name = "FM Radio";
            Frequency = _minFrequencyLimit;
        }
        public RadioFM(string name) : base(name)
        {
            Frequency = _minFrequencyLimit;
        }
        public RadioFM(string name, bool isPoweredOn, double frequency, int volume, double[] installedFrequency)
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
        public RadioFM(RadioFM other) : base(other)
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
                MessageUtils.PanicMessage("Error: Frequency goes beyond FM waves");
                ConsoleUtils.WaitForEnter();
            }
        }
    }
}
