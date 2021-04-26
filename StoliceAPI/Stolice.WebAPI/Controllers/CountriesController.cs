using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stolice.Database.Models;
using Stolice.Services.Interfaces;
using Stolice.WebAPI.RequestsModels;
using System;
using System.Threading.Tasks;

namespace Stolice.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CountriesController : ControllerBase
    {
        // Dependencies Injection - wstrzykiwanie zależności do konstruktora
        private readonly ILogger<CountriesController> _logger;
        private readonly ICountryService _countriesServices;


        public CountriesController(ILogger<CountriesController> logger,
            ICountryService countriesServices)
        {
            _logger = logger;
            _countriesServices = countriesServices;
        }

        // Route #1
        [HttpGet]
        [Route("all-countries")]
        public ActionResult GetAllCountries()
        {
            var countries = _countriesServices.GetAllCountries();

            if (countries != null)
                return Ok(countries);

            return NotFound();
        }

        [HttpGet]
        [Route("country/{countryId}")]
        public ActionResult GetCountryById([FromRoute] int countryId)
        {
            var country = _countriesServices.GetCountryById(countryId);

            if (country != null)
                return Ok(country);

            return NotFound();
        }

        [HttpPost]
        [Route("create")]
        public ActionResult CreateNewCountry(
            [FromBody] CountryRequestModel countryRequestModel)
        {
            var now = DateTime.UtcNow;
            var country = new Country
            {
                CountryName = countryRequestModel.CountryName,
                CreatedOn = now,
                CapitalId = countryRequestModel.CapitalId
            };

            _countriesServices.CreateCountry(country);

            return Ok($"{country.CountryName} zostało utworzone!");

        }

        [HttpDelete]
        [Route("delete/{countryId}")]
        public ActionResult DeleteCapitalById([FromRoute] int countryId)
        {
            _countriesServices.DeleteCountry(countryId);
            return Ok($"Kraj o ID = {countryId} zostało usunięte!");
        }

        [HttpGet]
        [Route("where/capital/is/{capitalId}")]
        public async Task<ActionResult<Country>> GetCountryWhereCapitalIdIs([FromRoute] int capitalId)
        {
            var countries = await _countriesServices.GetCountriesWhereCapitalIs(capitalId);

            if (countries != null)
            {
                return Ok(countries);
            }

            return NotFound();
        }

        [HttpPut]
        [Route("country/edit/{countryId}")]
        public async Task<ActionResult<Country>> UpdateCountry(
            [FromRoute] int countryId, Country country)
        {
            try
            {
                if (countryId != country.Id)
                {
                    return BadRequest();
                }
                var countryToUpdate = _countriesServices.GetCountryById(countryId);

                if (countryToUpdate == null)
                {
                    return NotFound($"Stolica z ID = {countryId} nie znaleziona!");
                }

                return await _countriesServices.UpdateCountry(country);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error!");
            }
        }
    }
}
