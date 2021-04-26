using Stolice.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stolice.Services.Interfaces
{
    public interface ICountryService
    {
        public List<Country> GetAllCountries();
        public Country GetCountryById(int countryId);
        public void CreateCountry(Country country);
        public void DeleteCountry(int countryId);

        public Task<IEnumerable<Country>> GetCountriesWhereCapitalIs(int capitalId);
        public Task<Country> UpdateCountry(Country country);
    }
}
