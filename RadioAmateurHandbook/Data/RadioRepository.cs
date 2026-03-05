using RadioAmateurHandbook.Radios;
using System.Text.Json;

namespace RadioAmateurHandbook.Data
{
    internal class RadioRepository
    {
        public void EnsureCreated()
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();
        }

        public bool HasData()
        {
            using var db = new AppDbContext();
            return db.Radios.Any();
        }

        public void Save(RadioFM fmRadio, RadioAM amRadio)
        {
            using var db = new AppDbContext();

            SaveRadio(db, "FM", fmRadio);
            SaveRadio(db, "AM", amRadio);

            db.SaveChanges();
        }

        private void SaveRadio(AppDbContext db, string type, Radio radio)
        {
            var entity = db.Radios.FirstOrDefault(r => r.Type == type);

            if (entity == null)
            {
                entity = new RadioEntity { Type = type };
                db.Radios.Add(entity);
            }

            entity.Name = radio.Name;
            entity.IsPoweredOn = radio.IsPoweredOn;
            entity.Frequency = radio.Frequency;
            entity.Volume = radio.Volume;
            entity.InstalledFrequenciesJson =
                JsonSerializer.Serialize(radio.InstalledFrequency);
        }

        public (RadioFM fm, RadioAM am) Load()
        {
            using var db = new AppDbContext();

            var fm = LoadRadio<RadioFM>(db, "FM");
            var am = LoadRadio<RadioAM>(db, "AM");

            return (fm, am);
        }

        private T LoadRadio<T>(AppDbContext db, string type) where T : Radio
        {
            var entity = db.Radios.FirstOrDefault(r => r.Type == type);

            if (entity == null)
                return (T)Activator.CreateInstance(typeof(T));

            var frequencies =
                JsonSerializer.Deserialize<double[]>(entity.InstalledFrequenciesJson)
                ?? new double[5];

            return (T)Activator.CreateInstance(
                typeof(T),
                entity.Name,
                entity.IsPoweredOn,
                entity.Frequency,
                entity.Volume,
                frequencies
            );
        }
    }
}