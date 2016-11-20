using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        WorldContext context;

        public WorldRepository(WorldContext context) {
            this.context = context;
        }

        public void AddStop(string tripName, Stop stop)
        {
            var trip = this.GetTripByName(tripName);

            if (trip != null)
            {
                trip.Stops.Add(stop);
                this.context.Add(stop);
            }
        }

        public void AddTrip(Trip trip)
        {
            this.context.Add(trip);
        }

        public IEnumerable<Trip> GetAllTrips() {
            return this.context.Trips.ToList();
        }

        public Trip GetTripByName(string tripName)
        {
            return this.context.Trips
                .Include(t => t.Stops)
                .Where(t => t.Name == tripName)
                .FirstOrDefault();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await this.context.SaveChangesAsync()) > 0;
        }
    }
}
