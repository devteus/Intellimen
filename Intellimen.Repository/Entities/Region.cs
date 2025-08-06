using System.ComponentModel.DataAnnotations.Schema;

namespace Intellimen.Repository.Entities
{
    [Table("Regiao")]
    public class Region
    {
        [Column("ide")]
        public long Ide { get; set; }
        [Column("pais")]
        public string Country { get; set; }
        [Column("estado")]
        public string State { get; set; }
        [Column("cidade")]
        public string City { get; set; }

        [Column("ide_usuario")]
        [ForeignKey(nameof(Profile))]
        public long IdeUser { get; set; }
        public virtual User User { get; set; }
    }
}
