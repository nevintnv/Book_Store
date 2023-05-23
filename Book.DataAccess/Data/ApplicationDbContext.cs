using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Book.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Book.Models;
using Microsoft.EntityFrameworkCore;

namespace Book.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product>  Products { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Action", DisplayOrder = 1 },
                new Category { CategoryId = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { CategoryId = 3, Name = "History", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product {
                    Id = 1,
                    Title = "The Adventures of Huckleberry Finn",
                    Author = "Mark Twain",
                    Description = "In the adventures of Huckleberry Finn, a sequel to the adventures of Tom Sawyer, Huck escapes from the clutches of his abusive drunk father ‘pap’, and the ‘sivilizing’ guardian widow Douglas",
                    ISBN = "SWD0009998888",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 1,
                    ImageUrl =""
                },
                                new Product
                                {
                                    Id = 2,
                                    Title = "The Great Gatsby",
                                    Author = "F. Scott Fitzgerald ",
                                    Description = "Fingerprint! Pocket Classics are perfect pocket-sized editions with complete original content.",
                                    ISBN = "FFDS00998886655",
                                    ListPrice = 90,
                                    Price = 87,
                                    Price50 = 85,
                                    Price100 = 80,
                                    CategoryId = 2,
                                    ImageUrl = ""
                                },
                                                new Product
                                                {
                                                    Id = 3,
                                                    Title = "1984",
                                                    Author = "George Orwell",
                                                    Description = "1984: A Novel, unleashes a unique plot as per which No One is Safe or Free. No place is safe to run or even hide from a dominating party leader, Big Brother, who is considered equal to God.",
                                                    ISBN = "ASD23445544",
                                                    ListPrice = 100,
                                                    Price = 90,
                                                    Price50 = 87,
                                                    Price100 = 82,
                                                    CategoryId = 3,
                                                    ImageUrl = ""
                                                }

                );
        }
    }
}
