using Microsoft.EntityFrameworkCore;
using VolleyData.Shared.DTOs;

namespace VolleyData.Server.Data
{
    public class ToDoDbContext: DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ToDoData> ToDoDbItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoData>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Description);
                entity.Property(e => e.Status);
                entity.Property(e => e.AttackTotal);
                entity.Property(e => e.AttackError);
                entity.Property(e => e.AttackBlock);
                entity.Property(e => e.AttackKill);
                entity.Property(e => e.ReceiveTotal);
                entity.Property(e => e.ReceiveError);
                entity.Property(e => e.ReceivePositiv);
                entity.Property(e => e.ReceiveExcellent);
                entity.Property(e => e.ServeTotal);
                entity.Property(e => e.ServeError);
                entity.Property(e => e.ServeKill);
                entity.Property(e => e.ActionsTotal);
                entity.Property(e => e.ActionsError);
                entity.Property(e => e.ErrorPercentage);
                entity.HasData(
                    new ToDoData
                    {
                        Id = 1,
                        Title = "Test",
                        Description = "Test",  
                        Status = false,
                        AttackTotal = 0,
                        AttackError = 0,
                        AttackBlock = 0,
                        AttackKill = 0,
                        ReceiveTotal = 0,
                        ReceiveError = 0,
                        ReceivePositiv = 0,
                        ReceiveExcellent = 0,
                        ServeTotal = 0,
                        ServeError = 0,
                        ServeKill = 0,
                        ActionsTotal = 0,
                        ActionsError = 0,
                        ErrorPercentage = 0
                    });

            });
        }
    }
}
