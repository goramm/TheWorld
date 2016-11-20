using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.API
{
    [Route("api/trips")]
    public class TripController : Controller
    {
        IWorldRepository repository;

        public TripController(IWorldRepository repository) {
            this.repository = repository;
        }

        [HttpGet("")]
        public IActionResult Get() {
            return Ok(Mapper.Map<IEnumerable<TripViewModel>> (this.repository.GetAllTrips()));
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel trip) {
            if (ModelState.IsValid) {
                var t = Mapper.Map<Trip>(trip);
                this.repository.AddTrip(t);

                if(await this.repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{trip.Name}", Mapper.Map<TripViewModel>(t));
                }

                return BadRequest(new Error("Fail to save shanges to the database"));
            }
            return BadRequest(ModelState);
        }
    }
}
