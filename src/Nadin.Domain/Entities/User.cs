using Nadin.Domain.Enums;
using SharedKernel.Common;

namespace Nadin.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public GenderType Gender { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public DateTime CreatedDate { get; set; }


        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
        public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
        public virtual IList<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
