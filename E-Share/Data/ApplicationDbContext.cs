using System;
using System.Collections.Generic;
using System.Text;
using E_Share.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Share.Data
{
    public class ApplicationDbContext : IdentityDbContext<
            ApplicationUser, ApplicationRole, string,
            ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
            ApplicationRoleClaim, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<RechargeStory> RechargeStories { get; set; }
        public DbSet<Avalailable> Avalailable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region modelbuilder
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });
            #endregion

            const string ROLEADMIN_ID = "3f24398b-2651-449f-a9cb-5e3085746795";
            const string ROLEUSER_ID = "19d7d7b6-10f3-455c-8f40-010bf5970143";
            const string ADMIN_ID = "4585db44-9cde-45fb-8777-4df85f8ca7b6";
            const string USER_ID = "445f85b8c-71fb-43f8-b025-080888bee57d";
            const string DEFAULT_PWD = "Password1!";

            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole { Id = ROLEADMIN_ID, Name = "Admin", NormalizedName = "ADMIN".ToUpper() },
                                                           new ApplicationRole { Id = ROLEUSER_ID, Name = "User", NormalizedName = "USER".ToUpper() });

            var hasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "admin@test.it",
                NormalizedUserName = "ADMIN@TEST.IT",
                Email = "admin@test.it",
                NormalizedEmail = "ADMIN@TEST.IT",
                Name = "admin",
                Surname = "test",
                Credit = 100,
                DateOfBirth = DateTime.Now,
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, DEFAULT_PWD),
                SecurityStamp = string.Empty
            },
            new ApplicationUser
            {
                Id = USER_ID,
                UserName = "user@test.it",
                NormalizedUserName = "USER@TEST.IT",
                Email = "user@test.it",
                NormalizedEmail = "USER@TEST.IT",
                Name = "user",
                Surname = "test",
                Credit = 100,
                DateOfBirth = DateTime.Now,
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, DEFAULT_PWD),
                SecurityStamp = string.Empty
            });

            modelBuilder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole
            {
                RoleId = ROLEADMIN_ID,
                UserId = ADMIN_ID
            },
            new IdentityUserRole<string>
            {
                RoleId = ROLEUSER_ID,
                UserId = USER_ID
            });

            modelBuilder.Entity<Status>().HasData(new Status() { Id = 1, Description = "Disponibile"},
                                                  new Status() { Id = 2, Description = "In uso"},
                                                  new Status() { Id = 3, Description = "Manutenzione"});


            modelBuilder.Entity<Category>().HasData(new Category() { Id = 1, Description = "e-scooter", Cost = 0.5 },
                                                  new Category() { Id = 2, Description = "e-bike", Cost = 1},
                                                  new Category() { Id = 3, Description = "e-car", Cost = 1.5 });

            modelBuilder.Entity<Province>().HasData(new Province() { Id = 1, Name = "Novara", Abbreviation ="NO" },
                                                    new Province() { Id = 2, Name = "Vercelli", Abbreviation = "Vercelli"});

            modelBuilder.Entity<City>().HasData(new City() { Id = 1, Name = "Novara", ProvinceId = 1 },
                                                new City() { Id = 2, Name = "Vercelli", ProvinceId = 2 });


            modelBuilder.Entity<Vehicle>().HasData(new Vehicle() { Id = 1, CityId = 1, BatteryResidue = 100, StatusId = 1, CategoryId = 1, Code = "A1", Latitude = 45.4510977, Longitude = 8.6223253 },
                                                   new Vehicle() { Id = 2, CityId = 1, BatteryResidue = 100, StatusId = 1, CategoryId = 2, Code = "B1", Latitude = 45.452404, Longitude = 8.621320 },
                                                   new Vehicle() { Id = 3, CityId = 1, BatteryResidue = 100, StatusId = 1, CategoryId = 3, Code = "C1", Latitude = 45.448110, Longitude = 8.625942 },
                                                   new Vehicle() { Id = 4, CityId = 2, BatteryResidue = 100, StatusId = 1, CategoryId = 1, Code = "A2", Latitude = 45.330682, Longitude = 8.422057 },
                                                   new Vehicle() { Id = 5, CityId = 2, BatteryResidue = 100, StatusId = 1, CategoryId = 2, Code = "B2", Latitude = 45.330515, Longitude = 8.423357 },
                                                   new Vehicle() { Id = 6, CityId = 2, BatteryResidue = 100, StatusId = 1, CategoryId = 3, Code = "C2", Latitude = 45.330052, Longitude = 8.421402 });

            modelBuilder.Entity<Avalailable>().HasData(new Avalailable() { Id = 1, CategoryId = 1, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Monday },
                                                       new Avalailable() { Id = 2, CategoryId = 1, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Tuesday },
                                                       new Avalailable() { Id = 3, CategoryId = 1, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Wednesday },
                                                       new Avalailable() { Id = 4, CategoryId = 1, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Thursday },
                                                       new Avalailable() { Id = 5, CategoryId = 1, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Friday },
                                                       new Avalailable() { Id = 6, CategoryId = 1, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Saturday },
                                                       new Avalailable() { Id = 7, CategoryId = 1, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Sunday },
                                                       new Avalailable() { Id = 8, CategoryId = 2, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Monday },
                                                       new Avalailable() { Id = 9, CategoryId = 2, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Tuesday },
                                                       new Avalailable() { Id = 10, CategoryId = 2, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Wednesday },
                                                       new Avalailable() { Id = 11, CategoryId = 2, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Thursday },
                                                       new Avalailable() { Id = 12, CategoryId = 2, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Friday },
                                                       new Avalailable() { Id = 13, CategoryId = 2, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Saturday },
                                                       new Avalailable() { Id = 14, CategoryId = 2, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Sunday },
                                                       new Avalailable() { Id = 15, CategoryId = 3, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Monday },
                                                       new Avalailable() { Id = 16, CategoryId = 3, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Tuesday },
                                                       new Avalailable() { Id = 17, CategoryId = 3, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Wednesday },
                                                       new Avalailable() { Id = 18, CategoryId = 3, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Thursday },
                                                       new Avalailable() { Id = 19, CategoryId = 3, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Friday },
                                                       new Avalailable() { Id = 20, CategoryId = 3, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Saturday },
                                                       new Avalailable() { Id = 21, CategoryId = 3, CityId = 1, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Sunday },
                                                       new Avalailable() { Id = 22, CategoryId = 1, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Monday },
                                                       new Avalailable() { Id = 23, CategoryId = 1, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Tuesday },
                                                       new Avalailable() { Id = 24, CategoryId = 1, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Wednesday },
                                                       new Avalailable() { Id = 25, CategoryId = 1, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Thursday },
                                                       new Avalailable() { Id = 26, CategoryId = 1, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Friday },
                                                       new Avalailable() { Id = 27, CategoryId = 1, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Saturday },
                                                       new Avalailable() { Id = 28, CategoryId = 1, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Sunday },
                                                       new Avalailable() { Id = 29, CategoryId = 2, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Monday },
                                                       new Avalailable() { Id = 30, CategoryId = 2, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Tuesday },
                                                       new Avalailable() { Id = 31, CategoryId = 2, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Wednesday },
                                                       new Avalailable() { Id = 32, CategoryId = 2, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Thursday },
                                                       new Avalailable() { Id = 33, CategoryId = 2, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Friday },
                                                       new Avalailable() { Id = 34, CategoryId = 2, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Saturday },
                                                       new Avalailable() { Id = 35, CategoryId = 2, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Sunday },
                                                       new Avalailable() { Id = 36, CategoryId = 3, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Monday },
                                                       new Avalailable() { Id = 37, CategoryId = 3, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Tuesday },
                                                       new Avalailable() { Id = 38, CategoryId = 3, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Wednesday },
                                                       new Avalailable() { Id = 39, CategoryId = 3, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Thursday },
                                                       new Avalailable() { Id = 40, CategoryId = 3, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Friday },
                                                       new Avalailable() { Id = 41, CategoryId = 3, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Saturday },
                                                       new Avalailable() { Id = 42, CategoryId = 3, CityId = 2, StartService = new TimeSpan(06, 00, 00), EndService = new TimeSpan(23, 00, 00), DayOfWeek = DayOfWeek.Sunday });
        }
    }
}
