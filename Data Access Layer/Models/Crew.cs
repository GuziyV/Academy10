using Data_Access_Layer.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    public class Crew : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public virtual Pilot Pilot { get; set; }

        [Required]
        public virtual List<Stewardess> Stewardesses { get; set; }
    }
}
