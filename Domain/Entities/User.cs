using FINALPROJECT.Domain.Enums;

namespace FINALPROJECT.Domain.Entities
{
    public class User : Auditables
    {
        public string Email { get; set; } = default!;
        public string Name { get; set; } 
        public string PasswordHash { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public Roles Role { get; set; } 
    }
}
