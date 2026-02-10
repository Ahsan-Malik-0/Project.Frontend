using System.Net.Sockets;

namespace Project.Frontend.Shared.Model
{
    public class Event
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime Date { get; set; }
        public required string Status { get; set; }
        public string? Message { get; set; }
        public Guid SocietyId { get; set; }
        public Society? Society { get; set; }
        public virtual ICollection<EventRequirement> Requirements { get; set; } = new List<EventRequirement>();
    }
}
