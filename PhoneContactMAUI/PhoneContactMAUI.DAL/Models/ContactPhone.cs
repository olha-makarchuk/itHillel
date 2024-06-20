using System.ComponentModel.DataAnnotations;

namespace PhoneContactMAUI.DAL.Models
{
	public class PhoneContact
    {
		[Key]
		public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
    }
}
