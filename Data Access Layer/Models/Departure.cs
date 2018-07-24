using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Departure : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public virtual Flight Flight { get; set; }

        [Required]
        public virtual DateTime TimeOfDeparture { get; set; }

        [Required]
        public virtual Crew Crew { get; set; }

        [Required]
        public virtual Plane Plane { get; set; }
    }
}
