using Api.Models;

namespace Api.Services
{
    public interface IHondaWmiService
    {
        List<Wmi>? GetAllWmisFromJsonFile();
        List<string> GetCountries();
    }
}
