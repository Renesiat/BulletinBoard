using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BulletinBoard.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Announcement> Announcements { get; set; }
    }
}
