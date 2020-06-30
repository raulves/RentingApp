using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Helpers
{
    public class DataInitializers
    {
        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();
        }
        public static bool DeleteDatabase(AppDbContext context)
        {
            return context.Database.EnsureDeleted();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var roles = new (string roleName, string roleDisplayName)[]
            {
                ("user", "User"),
                ("admin", "Admin")
            };

            foreach (var (roleName, roleDisplayName) in roles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole()
                    {
                        Name = roleName,
                        DisplayName = roleDisplayName
                    };

                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed!");
                    }
                }

            }


            var users = new (string name, string password, string firstName, string lastName)[]
            {
                ("vesinurm.raul@gmail.com", "Kala.maja.2020", "Raul", "Vesinurm"),
                ("test@test.com", "Kala.maja.2020", "Test", "Test it is"),
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByEmailAsync(userInfo.name).Result;
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Email = userInfo.name,
                        UserName = userInfo.name,
                        FirstName = userInfo.firstName,
                        LastName = userInfo.lastName,
                        EmailConfirmed = true
                    };
                    var result = userManager.CreateAsync(user, userInfo.password).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed!");
                    }
                }

                var roleResult = userManager.AddToRoleAsync(user, "admin").Result;
                roleResult  = userManager.AddToRoleAsync(user, "user").Result;
            }
            
        }
        
        public static void SeedData(AppDbContext context)
        {
            var buildingGuid = new Guid("589f6879-2bbf-49f8-bb03-b0bb97ab3e74");
            var clothesGuid = new Guid("a295eea6-f9f3-49dc-9f07-d7ae68d6a57a");
            var homeMachinesGuid = new Guid("061b75d5-1a7d-4eed-8452-7c9c0974604c");
            var hobbiesGuid = new Guid("1773092b-5dfd-4727-a9d3-ca554ee60454");
            var homeGuid = new Guid("d2f4086e-ba59-4f43-8b24-5ba40c46168a");
            var audioGuid = new Guid("d69bbc42-5826-410a-b3fe-c26125aee743");
            var computersGuid = new Guid("7b23ff29-b3b3-4f03-8837-c0dc7039c752");
            var kidsGuid = new Guid("07fa5a80-ba36-4c2d-bb35-3e950e5873c4");
            var booksGuid = new Guid("2909d8f3-2264-4821-8620-deeed4ea134c");
            var constructionGuid = new Guid("0f1e59b5-4ae0-4196-a4fe-1cf1310ba4d2");
            var toolsGuid = new Guid("1fef582f-b1b9-40ca-bdaf-5b0414cbeba0");
            
            /*
            // Insert predefined base Categories
            var categories = new Category[]
            {
                new Category()
                {
                    Description = "Tööriistad, masinad, ehitus",
                },
                new Category()
                {
                    Description = "Riided ja jalatsid",
                },
                new Category()
                {
                    Description = "Kodumasinad",
                },
                new Category()
                {
                    Description = "Harrastused ja vaba aeg",
                },
                new Category()
                {
                    Description = "Kodu",
                },
                new Category()
                {
                    Description = "Audio/video",
                },
                new Category()
                {
                    Description = "Arvutid",
                },
                new Category()
                {
                    Description = "Lastekaubad",
                },
                new Category()
                {
                    Description = "Raamatud, ajalehed",
                },
                
            };
            
            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }
            context.SaveChanges();
            */
            // ----------------------------------------------------------------------------------------------------
            // Insert predefined child Categories to Base categories
            
            /*
            // Add categories to Tööriistad, masinad, ehitus
            var buildingCategories = new Category[]
            {
                new Category()
                {
                    Description = "Ehitusmasinad",
                    ParentCategoryId = buildingGuid
                },
                new Category()
                {
                    Description = "Tööriistad",
                    ParentCategoryId = buildingGuid
                },
                new Category()
                {
                    Description = "Muud masinad ja seadmed",
                    ParentCategoryId = buildingGuid
                },
                
            };
            
            foreach (var category in buildingCategories)
            {
                context.Categories.Add(category);
            }


            // Add categories to Riided ja jalatsid
            var clothesCategories = new Category[]
            {
                new Category()
                {
                    Description = "Karnevali kostüümid",
                    ParentCategoryId = clothesGuid
                },
                new Category()
                {
                    Description = "Beebid",
                    ParentCategoryId = clothesGuid
                },
                new Category()
                {
                    Description = "Lapsed",
                    ParentCategoryId = clothesGuid
                },
                new Category()
                {
                    Description = "Naised",
                    ParentCategoryId = clothesGuid
                },
                new Category()
                {
                    Description = "Mehed",
                    ParentCategoryId = clothesGuid
                },
                
            };
            
            foreach (var category in clothesCategories)
            {
                context.Categories.Add(category);
            }
            
            
            // Add categories to Kodumasinad
            var homeMachinesCategories = new Category[]
            {
                new Category()
                {
                    Description = "Tolmuimejad",
                    ParentCategoryId = homeMachinesGuid
                },
                new Category()
                {
                    Description = "Veekeetjad ja kohvimasinad",
                    ParentCategoryId = homeMachinesGuid
                },
                new Category()
                {
                    Description = "Õmblusmasinad",
                    ParentCategoryId = homeMachinesGuid
                },
                new Category()
                {
                    Description = "Muud kodumasinad",
                    ParentCategoryId = homeMachinesGuid
                },

            };
            
            foreach (var category in homeMachinesCategories)
            {
                context.Categories.Add(category);
            }
            
            
            // Add categories to Harrastused ja vaba aeg
            var hobbiesCategories = new Category[]
            {
                new Category()
                {
                    Description = "Sporditarbed",
                    ParentCategoryId = hobbiesGuid
                },
                new Category()
                {
                    Description = "Mängud",
                    ParentCategoryId = hobbiesGuid
                },
                new Category()
                {
                    Description = "Reisi - ja matkatarbed",
                    ParentCategoryId = hobbiesGuid
                },
                new Category()
                {
                    Description = "Muud harrastused",
                    ParentCategoryId = hobbiesGuid
                },

            };
            
            foreach (var category in hobbiesCategories)
            {
                context.Categories.Add(category);
            }
            
            
            // Add categories to Kodu
            var homeCategories = new Category[]
            {
                new Category()
                {
                    Description = "Mööbel",
                    ParentCategoryId = homeGuid
                },
                new Category()
                {
                    Description = "Aed ja õu",
                    ParentCategoryId = homeGuid
                },
                new Category()
                {
                    Description = "Köögitarbed",
                    ParentCategoryId = homeGuid
                },

            };
            
            foreach (var category in homeCategories)
            {
                context.Categories.Add(category);
            }
            
            
            // Add categories to Audio/video
            var audioCategories = new Category[]
            {
                new Category()
                {
                    Description = "Televiisorid",
                    ParentCategoryId = audioGuid
                },
                new Category()
                {
                    Description = "Mängukonsoolid",
                    ParentCategoryId = audioGuid
                },
                new Category()
                {
                    Description = "Helitehnika",
                    ParentCategoryId = audioGuid
                },

            };
            
            foreach (var category in audioCategories)
            {
                context.Categories.Add(category);
            }
            
            
            // Add categories to Arvutid
            var computersCategories = new Category[]
            {
                new Category()
                {
                    Description = "Arvutimängud",
                    ParentCategoryId = computersGuid
                },
                new Category()
                {
                    Description = "Lauaarvutid",
                    ParentCategoryId = computersGuid
                },
                new Category()
                {
                    Description = "Sülearvutid",
                    ParentCategoryId = computersGuid
                },
                new Category()
                {
                    Description = "Tahvelarvutid",
                    ParentCategoryId = computersGuid
                },
                new Category()
                {
                    Description = "Monitorid",
                    ParentCategoryId = computersGuid
                },

            };
            
            foreach (var category in computersCategories)
            {
                context.Categories.Add(category);
            }
            
            
            // Add categories to Lastekaubad
            var kidsCategories = new Category[]
            {
                new Category()
                {
                    Description = "Lastetarbed",
                    ParentCategoryId = kidsGuid
                },
                new Category()
                {
                    Description = "Koolikaubad",
                    ParentCategoryId = kidsGuid
                },
                new Category()
                {
                    Description = "Mänguasjad",
                    ParentCategoryId = kidsGuid
                },

            };
            
            foreach (var category in kidsCategories)
            {
                context.Categories.Add(category);
            }
            
            
            // Add categories to Raamatud, ajalehed
            var booksCategories = new Category[]
            {
                new Category()
                {
                    Description = "Raamatud",
                    ParentCategoryId = booksGuid
                },
                new Category()
                {
                    Description = "Ajalehed",
                    ParentCategoryId = booksGuid
                },
                new Category()
                {
                    Description = "Ajakirjad",
                    ParentCategoryId = booksGuid
                },

            };
            
            foreach (var category in booksCategories)
            {
                context.Categories.Add(category);
            }
            
            
            context.SaveChanges();
            */
            
            // ----------------------------------------------------------------------------------------------------
            
            /*
            // Add categories to Ehitusmasinad
            var constructionCategories = new Category[]
            {
                new Category()
                {
                    Description = "Ekskavaatorid",
                    ParentCategoryId = constructionGuid
                },
                new Category()
                {
                    Description = "Laadurid",
                    ParentCategoryId = constructionGuid
                },
                new Category()
                {
                    Description = "Generaatorid",
                    ParentCategoryId = constructionGuid
                },
                new Category()
                {
                    Description = "Muud ehitusmasinad",
                    ParentCategoryId = constructionGuid
                },

            };
            
            foreach (var category in constructionCategories)
            {
                context.Categories.Add(category);
            }
            
            
            // Add categories to Tööriistad
            var toolsCategories = new Category[]
            {
                new Category()
                {
                    Description = "Käsitööriistad",
                    ParentCategoryId = toolsGuid
                },
                new Category()
                {
                    Description = "Elektritööriistad",
                    ParentCategoryId = toolsGuid
                },
                new Category()
                {
                    Description = "Metsatööriistad",
                    ParentCategoryId = toolsGuid
                },
                new Category()
                {
                    Description = "Puutöömasinad",
                    ParentCategoryId = toolsGuid
                },
                new Category()
                {
                    Description = "Muud tööriistad",
                    ParentCategoryId = toolsGuid
                },

            };
            
            foreach (var category in toolsCategories)
            {
                context.Categories.Add(category);
            }
            
            
            context.SaveChanges();
            */
            
            // ----------------------------------------------------------------------------------------------------
            
            /*
            // Add RentalPeriods to database
            var rentalPeriods = new RentalPeriod[]
            {
                new RentalPeriod()
                {
                    Description = "Päev",
                    PeriodStart = 1,
                    PeriodEnd = 7
                },
                new RentalPeriod()
                {
                    Description = "Nädal",
                    PeriodStart = 7,
                    PeriodEnd = 30
                },
                new RentalPeriod()
                {
                    Description = "Kuu",
                    PeriodStart = 30,
                    PeriodEnd = 365
                },
            };
            
            foreach (var rentalPeriod in rentalPeriods)
            {
                context.RentalPeriods.Add(rentalPeriod);
            }
            
            context.SaveChanges();
            */
            
            // ----------------------------------------------------------------------------------------------------
            
            // Add PaymentMethods to database
            /*
            var paymentTypes = new PaymentType[]
            {
                new PaymentType()
                {
                    Description = "Kaardimakse"
                },
                new PaymentType()
                {
                    Description = "Sularaha"
                },
                new PaymentType()
                {
                    Description = "E-konto"
                },
                new PaymentType()
                {
                    Description = "Panga ülekanne"
                },
            };
            
            foreach (var paymentType in paymentTypes)
            {
                context.PaymentTypes.Add(paymentType);
            }
            
            context.SaveChanges();
            */
            
            // ----------------------------------------------------------------------------------------------------
            // Add ProductDescriptions to database
            /*
            var productDescriptions = new ProductDescription[]
            {
                new ProductDescription()
                {
                    Description = "Pikkus,(mm)"
                },
                new ProductDescription()
                {
                    Description = "Laius,(mm)"
                },
                new ProductDescription()
                {
                    Description = "Kõrgus,(mm)"
                },
                new ProductDescription()
                {
                    Description = "Mass,(kg)"
                },
                new ProductDescription()
                {
                    Description = "Mootori võimsus, kW"
                },
            };
            
            foreach (var productDescription in productDescriptions)
            {
                context.ProductDescriptions.Add(productDescription);
            }
            
            context.SaveChanges();
            */
        }
        

    }
}