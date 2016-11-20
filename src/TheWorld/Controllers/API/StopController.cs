using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;

namespace TheWorld.Controllers.API
{
    [Route("/api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private IWorldRepository repository;
        private GeoCoordsService geoService;

        public StopController(IWorldRepository repository, GeoCoordsService geoService) {
            this.repository = repository;
            this.geoService = geoService;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            var trip = this.repository.GetTripByName(tripName);
            if(trip != null)
            {
                return Ok(trip.Stops.OrderBy(s => s.Order).ToList());
            }
            return NotFound(new Error($"Not found Stops for the Trip named: {tripName}", 404));
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName, [FromBody]Stop stop)
        {
            if (ModelState.IsValid)
            {

                var result = await this.geoService.GeoCoordsAsync(stop.Name);

                if (result.Success)
                {
                    stop.Lat = result.Lat;
                    stop.Long = result.Long;
                }

                this.repository.AddStop(tripName, stop);

                if (await this.repository.SaveChangesAsync())
                {
                    return Created($"/api/trips/{tripName}/stops/{stop.Name}", stop);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
