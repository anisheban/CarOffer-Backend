using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HondaWmi : ControllerBase
    {
        private readonly IHondaWmiService _hondaWmiService;

        public HondaWmi(IHondaWmiService hondaWmiService)
        {
            _hondaWmiService = hondaWmiService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string country = null, string search = null)
        {
            List<Wmi> hondaWmis = _hondaWmiService.GetAllWmisFromJsonFile();
            if (!string.IsNullOrWhiteSpace(country))
            {
                hondaWmis = hondaWmis.Where(x => x.Country == country).ToList();
            }
            if (!string.IsNullOrWhiteSpace(search))
            {
                hondaWmis = hondaWmis.Where(x => x.WMI.ToUpper().Contains(search.ToUpper())).ToList();
            }

            return Ok(
                    hondaWmis
                    .OrderBy(x => x.CreatedOn)
                    .ThenBy(x => x.WMI)
                    .ToList()
                );
        }

        [HttpGet("countries")]
        public async Task<IActionResult> Countries()
        {
            return Ok(_hondaWmiService.GetCountries());
        }

    }
}
