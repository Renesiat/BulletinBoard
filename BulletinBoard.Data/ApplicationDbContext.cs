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
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-RSQ2I6C\\MSSQLSERVER01;Database=BulletinBoardDb;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }
    }
}
