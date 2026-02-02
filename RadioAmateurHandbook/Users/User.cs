using RadioAmateurHandbook.Exceptions;
using RadioAmateurHandbook.Radios;
using RadioAmateurHandbook.Utils;

namespace RadioAmateurHandbook.Users
{
    internal abstract class User
    {
        private RadioFM fmRadio;
        private RadioAM amRadio;
        private Radio activeRadio;

        public User(RadioFM fm, RadioAM am)
        {
            this.fmRadio = fm;
            this.amRadio = am;

            this.activeRadio = fmRadio;
        }

        public Radio GetActiveRadio() => activeRadio;
        public void SelectFM() => activeRadio = fmRadio;
        public void SelectAM() => activeRadio = amRadio;

        public virtual bool CanTurnOn() { return true; }
        public virtual bool CanTurnOff() { return true; }
        public virtual bool CanSetVolume() { return true; }
        public virtual bool CanSetFrequency() { return true; }
        public virtual bool CanLoadFrequency() { return true; }
        public virtual bool CanSaveFrequency() { return true; }

        public bool IsPoweredOn() { return activeRadio.IsPoweredOn(); }
        public void TurnOn()
        {
            if (!CanTurnOn())
            {
                throw new PermissionDeniedException("turn on");
            }
            activeRadio.TurnOn();
        }
        public void TurnOff()
        {
            if (!CanTurnOff())
            {
                throw new PermissionDeniedException("turn off");
            }
            activeRadio.TurnOff();
        }

        public double GetFrequency() { return activeRadio.GetFrequency(); }
        public void SetFrequency(double frequency)
        {
            if (!CanSetFrequency())
            {
                throw new PermissionDeniedException("change frequency");
            }
            activeRadio.SetFrequency(frequency);
        }

        public int GetVolume() { return activeRadio.GetIVolume(); }
        public void SetVolume(int volume)
        {
            if (!CanSetVolume())
            {
                throw new PermissionDeniedException("change volume");
            }
            activeRadio.SetVolume(volume);
        }

        public void LoadFrequency(int index)
        {
            if (!CanLoadFrequency())
            {
                throw new PermissionDeniedException("load frequency");
            }
            activeRadio.LoadFrequency(index);
        }
        public void SaveFrequency(int index, double newFrequency)
        {
            if (!CanSaveFrequency())
            {
                throw new PermissionDeniedException("save frequency");
            }
            activeRadio.SaveFrequency(index, newFrequency);
        }

        public string GetActiveRadioType() { return activeRadio == fmRadio ? "FM" : "AM"; }
    }
}
