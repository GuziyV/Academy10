using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTOs
{
    public class CrewDTO
    {
        public int Id { get; set; }
        [Required]
        public PilotDTO Pilot { get; set; }

        [Required]
        public List<StewardessDTO> Stewardesses { get; set; }
    }
}
