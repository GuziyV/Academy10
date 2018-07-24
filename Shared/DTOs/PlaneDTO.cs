using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTOs
{
    public class PlaneDTO
    {
        public int Id { get; set; }

        [Required]
        public PlaneTypeDTO PlaneType { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public TimeSpan LifeTime
        {
            get
            {
                return DateTime.Now - ReleaseDate;
            }
        }
    }
}
