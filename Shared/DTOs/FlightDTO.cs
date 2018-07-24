using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTOs
{
    public class FlightDTO
    {
        public int Number { get; set; }

        [Required, MaxLength(20)]
        public string DepartureFrom { get; set; }

        [Required]
        public DateTime TimeOfDeparture { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        public List<TicketDTO> Tickets { get; set; }
    }
}
