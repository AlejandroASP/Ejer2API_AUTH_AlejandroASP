using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ejer2API_AlejandroSerafinParedes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Ejer2API_AlejandroSerafinParedes.Data
{
    public class UsersContext : IdentityDbContext<IdentityUser>
    {
        public UsersContext(DbContextOptions<UsersContext> options)
             : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // para evitar a la hora de añadir la migración el error de KEY
            modelBuilder.Entity<Auth>().HasNoKey();
            List<IdentityRole> roles = new List<IdentityRole>{
                new IdentityRole{
                Name = "User",
                NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Name = "Ale",
                    NormalizedName = "ALE"
                },
                new IdentityRole
                {
                    Name = "manu",
                    NormalizedName = "MANU"
                }
             };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
            List<IdentityUser> users = new List<IdentityUser>
             {
                new IdentityUser
                {
                UserName = "user@ejercicio3.com",
                NormalizedUserName = "USER@EJERCICIO3.COM" 
                },
                new IdentityUser
                {
                    UserName = "ale@ejercicio3.com",
                    NormalizedUserName = "ALE@EJERCICIO3.COM"
                },
                new IdentityUser
                {
                    UserName = "manu@ejercicio3.com",
                    NormalizedUserName = "MANU@EJERCICIO3.COM"
                }
             };

            modelBuilder.Entity<IdentityUser>().HasData(users);
            var passwordHasher = new PasswordHasher<IdentityUser>();
            users[0].PasswordHash = passwordHasher.HashPassword(users[0],"Asdf1234!");
            users[1].PasswordHash = passwordHasher.HashPassword(users[1], "Asdf1234!");
            users[2].PasswordHash = passwordHasher.HashPassword(users[2], "Asdf1234!");

            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>{
                new IdentityUserRole<string>
                {
                    UserId= users[0].Id,
                    RoleId= roles[0].Id
                },
                new IdentityUserRole<string>
                {
                    UserId= users[1].Id,
                    RoleId= roles[1].Id
                },
                new IdentityUserRole<string>
                {
                    UserId= users[2].Id,
                    RoleId= roles[2].Id
                },
             };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Ejer2API_AlejandroSerafinParedes.Models.Auth> Auth { get; set; } = default!;
    }
}
