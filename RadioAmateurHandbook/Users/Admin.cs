namespace RadioAmateurHandbook.Users
{
    internal class Admin : User
    {
        public Admin() : base("Admin") { }

        public override bool CanSetVolume() { return false; }

        public override bool CanSetFrequency() { return false; }

        public override bool CanSaveFrequency() { return false; }

        public override bool CanLoadFrequency() { return false; }
    }
}
