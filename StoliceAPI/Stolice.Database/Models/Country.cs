using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stolice.Database.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CountryName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        // relacja 
        public int CapitalId { get; set; }
        public Capital Capital { get; set; }
    }
}
