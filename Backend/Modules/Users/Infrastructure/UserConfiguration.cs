using Backend.Modules.Users.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Modules.Users.Infrastructure;

public sealed class UserConfiguration
    : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", table =>
        {
            var allowedRoles = string.Join(",", Enum.GetValues<Role>().Cast<int>());

            table.HasCheckConstraint("CK_users_role", $"role IN ({allowedRoles})");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(x => x.Value)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();
            email.HasIndex(x => x.Value).IsUnique();
        });

        builder.OwnsOne(x => x.Name, name =>
        {
            name.Property(x => x.Value)
                .HasMaxLength(100)
                .HasColumnName("name")
                .IsRequired();
        });

        builder.OwnsOne(x => x.Surname, surname =>
        {
            surname.Property(x => x.Value)
                .HasMaxLength(100)
                .HasColumnName("surname")
                .IsRequired();
        });

        builder.OwnsOne(x => x.PasswordHash, password =>
        {
            password.Property(x => x.Value)
                .HasColumnName("password_hash")
                .IsRequired();
        });

        builder.Property(x => x.Role)
            .HasColumnName("role")
            .HasConversion<int>()
            .IsRequired();
    }
}
