
namespace Project.Frontend.SharedServices
{
    public class NonFinancial_Req
    {
        private List<string> types = new() {
            "LTs",
            "Labs",
            "Other"
        };
        public List<string> lts = new() {
            "LT-1",
            "LT-2",
            "LT-3",
            "LT-4",
            "LT-5",
            "LT-6",
            "LT-7",
            "LT-8",
            "LT-9",
            "LT-10",
            "LT-11",
            "LT-12",
            "LT-13",
            "LT-14",
            "Library",
            "Basement"
        };

        List<string> labs = new()
        {
            "LAB-1",
            "LAB-2",
            "LAB-3",
            "LAB-4",
            "LAB-5",
            "LAB-6",
            "LAB-7",
            "LAB-8",
            "LAB-9",
            "LAB-10",
            "LAB-11",
            "DLD-LAB",
        };

        List<string> others = new()
        {
            "Projector",
            "Whiteboard",
            "Video Conferencing",
            "Audio System",
            "Computer Access",
            "Technical Support",
            "Catering Services",
            "Accessibility Services",
            "Security Services",
            "Cleaning Services"
        };

        public List<string> GetAllTypes()
        {
            return types;
        }

        public List<string> GetAllLts()
        {
            return lts;
        }

        public List<string> GetAllLabs()
        {
            return labs;
        }
        
        public List<string> GetAllOthers()
        {
            return others;
        }
    }
    
}