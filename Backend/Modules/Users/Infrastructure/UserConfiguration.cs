using Backend.Modules.Users.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Modules.Users.Infrastructure;

public sealed class UserConfiguration
    : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(x => x.Value)
                .HasColumnName("email")
                .IsRequired();
        });

        builder.OwnsOne(x => x.Name, name =>
        {
            name.Property(x => x.Value)
                .HasColumnName("name")
                .IsRequired();
        });

        builder.OwnsOne(x => x.Surname, surname =>
        {
            surname.Property(x => x.Value)
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
            .IsRequired();
    }
}
