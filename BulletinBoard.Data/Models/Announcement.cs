﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Data.Models
{
    public class Announcement
    {
        public int Id { get; set; }

        
        public string ?Title { get; set; } = string.Empty;

        
        public string ?Description { get; set; } = string.Empty;

        public DateTime ?CreatedDate { get; set; } = DateTime.UtcNow;

        
        public string ?Status { get; set; } = "Active";

        
        public string ?Category { get; set; } = string.Empty;

        
        public string ?SubCategory { get; set; } = string.Empty;

    }
}
