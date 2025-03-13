using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL2Service.DAL.Sql.Entities
{
    [Table("Users")]
    public class UserEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // DB generated -- unique in all DB

        [Column("name")]
        public string Name { get; set; }

        [Column("password")]
        public string Password { get; set; }

       
    }
}
