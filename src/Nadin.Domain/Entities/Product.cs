using SharedKernel.Common;

namespace Nadin.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }

        public virtual User User { get; set; }
    }
}
