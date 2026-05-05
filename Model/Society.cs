using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Project.Frontend.Model
{
    public class Society
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public virtual ICollection<Member> Members { get; set; } = new List<Member>();
    }
}
