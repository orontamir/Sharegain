using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL2Service.DAL.Sql.Entities
{
    [Table("Signals")]
    public class SignalEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("TimeStemp")]
        public DateTime TimeStemp { get; set; }
        [Column("Time")]
        public string Time { get; set; }
        [Column("Date")]
        public string Date { get; set; }
        [Column("Value")]
        public double Value { get; set; }
        [Column("Type")]
        public string Type { get; set; }
        [Column("Error")]
        public bool Error { get; set; }
    }
}
