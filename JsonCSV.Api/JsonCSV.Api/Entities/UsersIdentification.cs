using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Security.Cryptography;

namespace JsonCSV.Api.Entities
{
	public class UsersIdentification
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[StringLength(10)]	
		public string UserName { get; set;}

        public string Name { get; set; }

        [Required]
		public string PasswordHash { get; set;}

		[Required]
		public string Role { get; set; }

		public string SetPassword(string password)
		{
			byte[] paswordBytes = Encoding.UTF8.GetBytes(password); 
			byte[] hashBytes = SHA256.Create().ComputeHash(paswordBytes);
			
			return Convert.ToBase64String(hashBytes);
		}

	}
}
