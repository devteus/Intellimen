using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intellimen.Repository.Entities
{
    [Table("Usuario")]
    public class User
    {
        [Key]
        [Column("ide")]
        public long Ide { get; set; }

        [Column("nome")]
        public string Name { get; set; }

        [Column("sobrenome")]
        public string Surname { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("senha")]
        public string Password { get; set; }
        [Column("ativo")]
        public bool Active { get; set; }

        [Column("data_cadastro")]
        public DateTime RegistrationDate { get; set; }

        [Column("is_parceiro")]
        public bool Partner { get; set; }

        [Column("tipo_relacao")]
        public int Relationship { get; set; }

        [Column("ide_perfil")]
        [ForeignKey(nameof(Profile))]
        public long IdeProfile { get; set; }
        public virtual Profile Profile { get; set; }
    }

}
