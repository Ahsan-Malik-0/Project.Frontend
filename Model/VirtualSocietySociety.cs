
using System.Text.Json.Serialization;

namespace Project.Frontend.Model
{
    public class VirtualSocietySociety
    {
        public Guid Id { get; set; }
        public Guid VirtualSocietyId { get; set; }
        [JsonIgnore]
        public VirtualSociety? VirtualSociety { get; set; }
        public Guid SocietyId { get; set; }
        [JsonIgnore] 
        public Society? Society { get; set; }
    }
}
