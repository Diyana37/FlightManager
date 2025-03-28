using FlightManager.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.ViewModels.Flights
{
    public class FlightBasicViewModel
    {
        public int Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime DepartureAt { get; set; }

        public DateTime LandAt { get; set; }

        public string Type { get; set; }

        public string UniqieId { get; set; }

        public string PilotName { get; set; }

        public int CapacityStandard { get; set; }

        public int CapacityBusiness { get; set; }

    }
}
