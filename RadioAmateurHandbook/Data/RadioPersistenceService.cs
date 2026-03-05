using RadioAmateurHandbook.Radios;

namespace RadioAmateurHandbook.Data
{
    internal class RadioPersistenceService
    {
        private readonly RadioRepository _repo;

        public RadioPersistenceService(RadioRepository repo)
        {
            _repo = repo;
        }

        public void EnsureCreated()
        {
            _repo.EnsureCreated();
        }

        public bool HasSavedData()
        {
            return _repo.HasData();
        }

        public bool TrySave(RadioFM fm, RadioAM am)
        {
            try
            {
                _repo.Save(fm, am);
                return true;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error saving data: {e.Message}");
                return false;
            }
        }

        public (RadioFM fm, RadioAM am)? TryLoad()
        {
            try
            {
                return _repo.Load();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error loading data: {e.Message}");
                return null;
            }
        }
    }
}