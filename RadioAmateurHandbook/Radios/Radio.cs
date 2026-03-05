namespace RadioAmateurHandbook.Radios
{
    internal abstract class Radio
    {
        private static int _maxVolume = 120;
        private static int _maxFrequencySlots = 5;

        public string Name { get; protected set; }
        public bool IsPoweredOn { get; protected set; }
        public double Frequency { get; protected set; }
        public int Volume { get; protected set; }
        public double[] InstalledFrequency { get; protected set; } = new double[_maxFrequencySlots];

        public Radio()
        {
            Name = "Radio";
            IsPoweredOn = false;
            Frequency = 100.0;
            Volume = 10;
            for (int i = 0; i < InstalledFrequency.Length; i++)
            {
                InstalledFrequency[i] = 0;
            }
        }
        public Radio(string name)
        {
            Name = name;
            IsPoweredOn = false;
            Frequency = 100.0;
            Volume = 10;
            InstalledFrequency = new double[_maxFrequencySlots];
        }
        public Radio(string name, bool isPoweredOn, double frequency, int volume, double[] installedFrequency)
        {
            Name = name;
            IsPoweredOn = isPoweredOn;
            Frequency = frequency;
            Volume = volume;
            InstalledFrequency = installedFrequency;
        }
        public Radio(Radio other)
        {
            Name = other.Name;
            IsPoweredOn = other.IsPoweredOn;
            Frequency = other.Frequency;
            Volume = other.Volume;
            InstalledFrequency = other.InstalledFrequency;
        }

        public virtual void Reset()
        {
            IsPoweredOn = false;
            Volume = 10;
            Frequency = 100.0;
            for (int i = 0; i < InstalledFrequency.Length; i++)
            {
                InstalledFrequency[i] = 0;
            }
        }

        public void TurnOn()
        {
            IsPoweredOn = true;
        }
        public void TurnOff()
        {
            IsPoweredOn = false;
        }

        public abstract void SetFrequency(double frequency);

        public void SetVolume(int volume)
        {
            if (volume < 0)
            {
                Volume = 0;
                throw new ArgumentOutOfRangeException(nameof(volume), "Volume cannot be negative.");
            }

            if (volume > _maxVolume)
            {
                throw new ArgumentOutOfRangeException(nameof(volume), $"Volume cannot exceed {_maxVolume}.");
            }

            Volume = volume;
        }

        public void SaveFrequency(int index, double newFrequency)
        {
            if (index < 0 || index >= InstalledFrequency.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"Index must be between 1 and {_maxFrequencySlots}.");
            }

            InstalledFrequency[index] = newFrequency;
        }
        public void LoadFrequency(int index)
        {
            if (index < 1 || index > InstalledFrequency.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"Index must be between 1 and {_maxFrequencySlots}.");
            }

            double value = InstalledFrequency[index - 1];

            if (value == 0)
            {
                throw new InvalidOperationException("Value by index is empty.");
            }

            SetFrequency(value);
        }

    }
}
