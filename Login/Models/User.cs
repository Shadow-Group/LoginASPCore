using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("UserName", TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Required]
        [Column("UserEmail", TypeName = "varchar(100)")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
