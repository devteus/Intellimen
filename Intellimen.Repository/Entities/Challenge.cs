using System.ComponentModel.DataAnnotations.Schema;

namespace Intellimen.Repository.Entities
{
    [Table("Desafio")]
    public class Challenge
    {
        [Column("ide")]
        public long Ide { get; set; }
        [Column("numero")]
        public int Number { get; set; }
        [Column("titulo")]
        public string Title { get; set; }
        [Column("descricao")]
        public string Description { get; set; }
    }
}
