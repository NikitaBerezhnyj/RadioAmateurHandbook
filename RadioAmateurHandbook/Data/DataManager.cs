using System;
using System.IO;
using System.Text.Json;
using RadioAmateurHandbook.Radios;
using RadioAmateurHandbook.Data.DTO;

namespace RadioAmateurHandbook.Data
{
    internal static class DataManager
    {
        private const string Filename = "radio_data.json";
        public static string GetFilename() { return Filename; }

        public static void CreateData()
        {
            if (!File.Exists(Filename))
            {
                File.WriteAllText(Filename, "{}");
            }
        }

        public static bool SaveData(RadioFM fmRadio, RadioAM amRadio)
        {
            try
            {
                var data = new RadioData
                {
                    FMRadio = new RadioDTO(fmRadio),
                    AMRadio = new RadioDTO(amRadio)
                };

                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(Filename, json);
                return true;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error saving data: {e.Message}");
                return false;
            }
        }

        public static bool LoadData(RadioFM fmRadio, RadioAM amRadio)
        {
            try
            {
                if (!File.Exists(Filename))
                {
                    Console.Error.WriteLine("Data file not found.");
                    return false;
                }

                string json = File.ReadAllText(Filename);
                var data = JsonSerializer.Deserialize<RadioData>(json);

                if (data != null)
                {
                    fmRadio.Load(data.FMRadio);
                    amRadio.Load(data.AMRadio);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error loading data: {e.Message}");
                return false;
            }
        }
    }
}
