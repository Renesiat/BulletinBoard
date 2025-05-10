using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Data.Models
{
    public class Announcement
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? Status { get; set; }

        public string? Category { get; set; }

        public string? SubCategory { get; set; }
    }
}
