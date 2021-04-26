using Stolice.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stolice.Services.Interfaces
{
    public interface ICapitalService
    {
        // 1. Pobierz wszystkie stolice z bazy danych
        public List<Capital> GetAllCapitalsToList();
        // 2. Pobierz stolice po ID
        public Capital GetCapitalById(int capitalId);
        // 3. Dodaj nową stolicę
        public void CreateNewCapital(Capital capital);
        // 4. Zaaktualizuj stolicę po ID
        public Task<Capital> UpdateCapital(Capital capital);
        // 5. Usuń stolicę po ID
        public void DeleteCapitalById(int capitalId);
    }
}
