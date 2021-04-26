using Microsoft.EntityFrameworkCore;
using Stolice.Database;
using Stolice.Database.Models;
using Stolice.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stolice.Services
{
    public class CapitalService : ICapitalService
    {
        // Dependency Injection - wstrzykiwanie zależności do konstruktora
        private readonly AppDbContext _db;

        public CapitalService(AppDbContext db)
        {
            _db = db;
        }

        // 1
        public List<Capital> GetAllCapitalsToList()
        {
            return _db.Capitals.ToList();
        }

        // 2
        public Capital GetCapitalById(int capitalId)
        {
            return _db.Capitals.Find(capitalId);
        }

        // 3
        public void CreateNewCapital(Capital capital)
        {
            _db.Capitals.Add(capital);
            _db.SaveChanges();
        }

        // 4
        public async Task<Capital> UpdateCapital(Capital capital)
        {
            var result = await _db.Capitals.FirstOrDefaultAsync(c => c.Id == capital.Id);

            if (result != null)
            {
                result.Name = capital.Name;

                await _db.SaveChangesAsync();

                return result;
            }

            return null;
        }

        // 5
        public void DeleteCapitalById(int capitalId)
        {
            var capital = _db.Capitals.Find(capitalId);

            if (capital != null)
            {
                _db.Remove(capital);
                _db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Nie odneleziono rekordu w bazie danych!");
            }
        }
    }
}
