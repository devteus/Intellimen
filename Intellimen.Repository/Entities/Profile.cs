using System.ComponentModel.DataAnnotations.Schema;

namespace Intellimen.Repository.Entities
{
    [Table("Perfil")]
    public class Profile
    {
        [Column("ide")]
        public long Ide { get; set; }
        [Column("nome")]
        public string Name { get; set; }
    }
}
