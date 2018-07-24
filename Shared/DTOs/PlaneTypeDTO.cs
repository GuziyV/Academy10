using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTOs
{
    public class PlaneTypeDTO
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Model { get; set; }

        public int NumberOfSeats { get; set; }

        public int LoadCapacity { get; set; }
    }
}
