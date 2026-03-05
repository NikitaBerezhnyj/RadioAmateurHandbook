namespace RadioAmateurHandbook.Users
{
    internal abstract class User
    {
        public string Role { get; protected set; }

        protected User(string role)
        {
            Role = role;
        }

        public virtual bool CanTurnOn() { return true; }
        public virtual bool CanTurnOff() { return true; }
        public virtual bool CanSetVolume() { return true; }
        public virtual bool CanSetFrequency() { return true; }
        public virtual bool CanLoadFrequency() { return true; }
        public virtual bool CanSaveFrequency() { return true; }
    }
}
