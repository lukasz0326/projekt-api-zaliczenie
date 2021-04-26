using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stolice.Database.Models;
using Stolice.Services.Interfaces;
using Stolice.WebAPI.RequestsModels;
using System;
using System.Threading.Tasks;

namespace Stolice.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CapitalsController : ControllerBase
    {
        // Dependencies Injection - wstrzykiwanie zależności do konstruktora
        private readonly ILogger<CapitalsController> _logger;
        private readonly ICapitalService _capitalServices;


        public CapitalsController(ILogger<CapitalsController> logger,
            ICapitalService capitalServices)
        {
            _logger = logger;
            _capitalServices = capitalServices;
        }

        // Route #1
        [HttpGet]
        [Route("all-capitals")]
        public ActionResult GetAllCapitals()
        {
            var capitals = _capitalServices.GetAllCapitalsToList();

            if (capitals != null)
                return Ok(capitals);

            return NotFound();
        }

        [HttpGet]
        [Route("capital/{capitalId}")]
        public ActionResult GetCapitalById([FromRoute] int capitalId)
        {
            var capital = _capitalServices.GetCapitalById(capitalId);

            if (capital != null)
                return Ok(capital);

            return NotFound();
        }

        [HttpPost]
        [Route("create")]
        public ActionResult CreateNewCapital(
            [FromBody] CapitalRequestModel capitalRequestModel)
        {
            var capital = new Capital
            {
                Name = capitalRequestModel.Name
            };

            _capitalServices.CreateNewCapital(capital);

            return Ok($"{capital.Name} została utworzona!");

        }

        [HttpDelete]
        [Route("delete/{capitalId}")]
        public ActionResult DeleteCapitalById([FromRoute] int capitalId)
        {
            _capitalServices.DeleteCapitalById(capitalId);
            return Ok($"Stolica o ID = {capitalId} została usunięta!");
        }

        [HttpPut]
        [Route("capital/edit/{capitalId}")]
        public async Task<ActionResult<Capital>> EditCapital([FromRoute] int capitalId, Capital capital)
        {
            //var now = DateTime.UtcNow;
            try
            {
                if (capitalId != capital.Id)
                {
                    return BadRequest();
                }
                var capitalToUpdate = _capitalServices.GetCapitalById(capitalId);

                if (capitalToUpdate == null)
                {
                    return NotFound($"Stolica z ID = {capitalId} nie znaleziona!");
                }

                return await _capitalServices.UpdateCapital(capital);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error!");
            }

        }
    }
}
