using System.Text.Json.Serialization;

namespace Project.Frontend.Model.DTOs
{
    public class CreateVirtualSocietyDto
    {
        public required string Name { get; set; }
        public string? Session { get; set; }
        public List<Guid> SocietyIds { get; set; } = new List<Guid>();
    }

    public class CheckSocietySelectedDto
    {
        public bool IsChecked { get; set; }
    }

    public class AddIdInVirtualSocietiesSocietyDto
    {
        //public Guid VirtualId { get; set; }
        public List<Guid> SocietyIds { get; set; } = new List<Guid>();
    }

    public class CreateVirtualSocietyRequisitionDto
    {
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public DateTime RequestedDate { get; set; }
        public decimal RequestAmount { get; set; }
        public Guid VirtualSocietyId { get; set; }
        [JsonIgnore]
        public VirtualSociety? VirtualSociety { get; set; }
    }

    public class ListOfSocietiesDto
    {
        public Guid SocietyId { get; set; }
        public required string SocietyName { get; set; }
    }
}
