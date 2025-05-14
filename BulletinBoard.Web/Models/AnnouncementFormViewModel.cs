using BulletinBoard.Data.Models;

namespace BulletinBoard.Web.Models
{
    public class AnnouncementFormViewModel
    {
        public Announcement Announcement { get; set; } = new();
        public List<string> Categories { get; set; } = new();
        public List<string> SubCategories { get; set; } = new();
        public string? SelectedCategory { get; set; }
    }
}
