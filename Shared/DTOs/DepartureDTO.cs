using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTOs
{
    public class DepartureDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public FlightDTO Flight { get; set; }

        [Required]
        public DateTime TimeOfDeparture { get; set; }

        [Required]
        public CrewDTO Crew { get; set; }

        [Required]
        public PlaneDTO Plane { get; set; }
    }
}
