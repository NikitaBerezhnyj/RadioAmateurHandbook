using Microsoft.EntityFrameworkCore;

namespace RadioAmateurHandbook.Data
{
    public class AppDbContext : DbContext
    {
        private static string _dataSource = "radio.db";

        public DbSet<RadioEntity> Radios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={_dataSource}");
    }
}