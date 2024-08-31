using DiaryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Data
{
    public class ApplicationDbContext: DbContext
    {
        /*
         * Four Steps to add a table
         * 1. Create a Model Class
         * 2. Add DB Set
         * 3. add-migration AddDiaryEntryTable
         * 4. update-database
         * **/
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        // Since we use DbSet, the ee framework will know that a table have the same schema as defined in DiaryEntry will need to be created
        // What should do after create this:
        // open the nuget project management console => use command ```add-migration [migration name]``` => it will record and make changes to our database
        // after the migration, we will need to use command ```update-database```, so the changes will commit to our database
        public DbSet<DiaryEntry> DiaryEntries { get; set; }

        /**
         Seeding data in development
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DiaryEntry>().HasData(
                new DiaryEntry { Id = 1, Title = "Went Hiking", Content="Went hiking with Joe!", Created=DateTime.Now },
                new DiaryEntry { Id = 2, Title = "Went Shopping", Content = "Went shopping with Joe!", Created = DateTime.Now },
                new DiaryEntry { Id = 3, Title = "Went Diving", Content = "Went diving with Joe!", Created = DateTime.Now }
            );
        }
    }
}
