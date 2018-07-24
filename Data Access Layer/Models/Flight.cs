using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        [Required]
        public string DepartureFrom { get; set; }

        [Required]
        public virtual DateTime TimeOfDeparture { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        public virtual List<Ticket> Tickets { get; set; }

    }
}
