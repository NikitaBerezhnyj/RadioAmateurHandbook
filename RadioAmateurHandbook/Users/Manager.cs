namespace RadioAmateurHandbook.Users
{
    internal class Manager : User
    {
        public Manager() : base("Manager") { }

        public override bool CanTurnOn() { return false; }

        public override bool CanTurnOff() { return false; }

        public override bool CanSetVolume() { return false; }

        public override bool CanSetFrequency() { return false; }
    }
}
