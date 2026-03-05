namespace RadioAmateurHandbook.Users
{
    internal class Director : User
    {
        public Director() : base("Director") { }

        public override bool CanTurnOn() { return false; }

        public override bool CanTurnOff() { return false; }

        public override bool CanSaveFrequency() { return false; }

        public override bool CanLoadFrequency() { return false; }
    }
}
