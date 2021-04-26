using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stolice.WebAPI.RequestsModels
{
    public class CountryRequestModel
    {
        public string CountryName { get; set; }
        public int CapitalId { get; set; }
    }
}
