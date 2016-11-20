using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldContextSeed
    {
        WorldContext context;

        public WorldContextSeed(WorldContext context) {
            this.context = context;
        }

        public async Task EnsureSeedData()
        {
            if (!this.context.Trips.Any()) {
                var trips = new Trip()
                {
                    Created = DateTime.UtcNow,
                    Name = "My Trip",
                    UserName = "",
                    Stops = new List<Stop>()
                    {
                        new Stop() { Name= "Zagreb", Arrival = new DateTime(2016, 1, 1), Lat = 45.815011, Long = 15.981919, Order = 0 },
                        new Stop() { Name= "Karlovac", Arrival = new DateTime(2016, 1, 2), Lat = 45.492897, Long = 15.555268, Order = 0 },
                        new Stop() { Name= "Rijeka", Arrival = new DateTime(2016, 1, 3), Lat = 45.327063, Long = 14.442176, Order = 0 },
                        new Stop() { Name= "Split", Arrival = new DateTime(2016, 1, 4), Lat = 43.508132, Long = 16.440193, Order = 0 },
                        new Stop() { Name= "Sarajevo", Arrival = new DateTime(2016, 1, 5), Lat = 43.856259, Long = 18.413076, Order = 0 },
                        new Stop() { Name= "Slavnoski Brod", Arrival = new DateTime(2016, 1, 6), Lat = 45.163143, Long = 18.011608, Order = 0 },
                        new Stop() { Name= "Zagreb", Arrival = new DateTime(2016, 1, 7), Lat = 45.815011, Long = 15.981919, Order = 0 },
                    }
                };
                this.context.Trips.Add(trips);
                this.context.Stops.AddRange(trips.Stops);
                this.context.SaveChanges();
            }
        }
    }
}
