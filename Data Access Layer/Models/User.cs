using System.ComponentModel.DataAnnotations.Schema;
using Data_Access_Layer.Interfaces;

namespace DAL.Models {
	public class User : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string IP { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
	}
}
