using Microsoft.EntityFrameworkCore;
using Stolice.Database;
using Stolice.Database.Models;
using Stolice.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stolice.Services
{
   public class CountryService : ICountryService
    {
        private readonly AppDbContext _db;

        public CountryService(AppDbContext db)
        {
            _db = db;
        }

        public List<Country> GetAllCountries()
        {
            return _db.Countries.ToList();
        }

        public Country GetCountryById(int countryId)
        {
            return _db.Countries.Find(countryId);
        }

        public void CreateCountry(Country country)
        {
            _db.Countries.Add(country);
            _db.SaveChanges();
        }

        public void DeleteCountry(int countryId)
        {
            var country = _db.Countries.Find(countryId);

            if (country != null)
            {
                _db.Remove(country);
                _db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Nie odnaleziono rekordu w bazie danych!");
            }
        }

       
        public async Task<IEnumerable<Country>> GetCountriesWhereCapitalIs(int capitalId)
        {
            var countries = await _db.Countries.Include(capital => capital.Capital).Where(c => c.CapitalId == capitalId).ToListAsync();
            return countries;
        }

        public async Task<Country> UpdateCountry(Country country)
        {
            var result = await _db.Countries.FirstOrDefaultAsync(c => c.Id == country.Id);

            if (result != null)
            {

                result.CountryName = country.CountryName;
                result.UpdatedOn = DateTime.UtcNow;
                result.CapitalId = country.CapitalId;

                await _db.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
