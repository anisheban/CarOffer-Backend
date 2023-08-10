using Api.Models;
using System.Text.Json;

namespace Api.Services
{
    public class HondaWmiService : IHondaWmiService
    {
        private const string JsonFileName = "./DataFiles/honda_wmi.json";

        /// <summary>
        /// Read the JSON file and return a list of Honda Wmis
        /// </summary>
        public List<Wmi>? GetAllWmisFromJsonFile()
        {
            // Check if the file exists
            if (System.IO.File.Exists(JsonFileName))
            {
                // Read the JSON file
                string jsonData = System.IO.File.ReadAllText(JsonFileName);
                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    // Deserialize the JSON content into a list of objects
                    List<Wmi> hondaWmis = JsonSerializer.Deserialize<List<Wmi>>(jsonData);
                    return hondaWmis;
                }
            }
            // Return null if the file does not exist or if the file is empty
            return null;
        }

        public List<string> GetCountries()
        {
            List<Wmi> hondaWmis = GetAllWmisFromJsonFile();

            List<string> countries = hondaWmis
                .Where(x => x.Country != null)
                .OrderBy(x => x.Country)
                .Select(x => x.Country)
                .Distinct()
                .ToList();
            return countries;
        }

    }
}
