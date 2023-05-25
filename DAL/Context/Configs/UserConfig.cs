using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configs;

// classe qui permet de configurer mon utilisateur

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasData(
            new User
            {
                Id = 1,
                Email = "admin@admin.be",
                Firstname = "Admin",
                Lastname = "Ator",
                Password = "Test123=",
                PhoneNumber = "01223345367543",
                Pseudo = "Adminator",
                Role = Enums.Roles.admin
                
            },
            new User
            {
                Id = 2,
                Email = "user@user.be",
                Firstname = "User",
                Lastname = "Ator",
                Password = "Test123=",
                PhoneNumber = "01223345367543",
                Pseudo = "Userator",
                Role = Enums.Roles.user
            }
        );
    }
}