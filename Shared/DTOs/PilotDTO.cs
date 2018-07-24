using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTOs
{
    public class PilotDTO
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        [Required, MaxLength(25)]
        public string Surname { get; set; }

        public int Experience { get; set; }
    }
}
