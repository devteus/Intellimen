using System.ComponentModel.DataAnnotations.Schema;

namespace Intellimen.Repository.Entities
{
    [Table("UsuarioDesafio")]
    public class ChallengeUser
    {
        [Column("ide")]
        public long Ide { get; set; }
        [Column("status")]
        public int Status { get; set; }
        [Column("data_inicio")]
        public DateTime StartDate { get; set; }
        [Column("data_conclusao")]
        public DateTime CompletionDate { get; set; }

        [Column("ide_desafio")]
        [ForeignKey(nameof(Challenge))]
        public long IdeChallenge { get; set; }
        public virtual Challenge Challenge { get; set; }
        
        [Column("ide_usuario")]
        [ForeignKey(nameof(User))]
        public long IdeUser { get; set; }
        public virtual User User { get; set; }
    }
}
