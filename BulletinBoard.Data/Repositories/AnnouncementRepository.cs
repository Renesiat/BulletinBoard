using BulletinBoard.Data.Interfaces;
using BulletinBoard.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Data.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly string _connectionString;

        public AnnouncementRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Announcement>> GetAllAsync()
        {
            var list = new List<Announcement>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("GetAllAnnouncements", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new Announcement
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    CreatedDate = reader.GetDateTime(3),
                    Status = reader.GetString(4),
                    Category = reader.GetString(5),
                    SubCategory = reader.GetString(6)
                });
            }

            return list;
        }

        public async Task<Announcement?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("GetAnnouncementById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Announcement
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    CreatedDate = reader.GetDateTime(3),
                    Status = reader.GetString(4),
                    Category = reader.GetString(5),
                    SubCategory = reader.GetString(6)
                };
            }

            return null;
        }

        public async Task AddAsync(Announcement announcement)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("AddAnnouncement", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Title", announcement.Title);
            command.Parameters.AddWithValue("@Description", announcement.Description);
            command.Parameters.AddWithValue("@Status", announcement.Status);
            command.Parameters.AddWithValue("@Category", announcement.Category);
            command.Parameters.AddWithValue("@SubCategory", announcement.SubCategory);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Announcement announcement)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("UpdateAnnouncement", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", announcement.Id);
            command.Parameters.AddWithValue("@Title", announcement.Title);
            command.Parameters.AddWithValue("@Description", announcement.Description);
            command.Parameters.AddWithValue("@Status", announcement.Status);
            command.Parameters.AddWithValue("@Category", announcement.Category);
            command.Parameters.AddWithValue("@SubCategory", announcement.SubCategory);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("DeleteAnnouncement", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
