using Intellimen.Repository.Entities;

namespace Intellimen.Business.DTOs
{
    public class UserDTO
    {
        public long Ide { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Active { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Partner { get; set; }
        public int Relationship { get; set; }
        public DateTimeOffset? ExpiresIn { get; set; }

        public UserDTO()
        {
        }

        public UserDTO(User user)
        {
            Ide = user.Ide;
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
            Password = user.Password;
            Active = user.Active;
            RegistrationDate = user.RegistrationDate;
            Partner = user.Partner;
            Relationship = user.Relationship;
        }
    }
}
