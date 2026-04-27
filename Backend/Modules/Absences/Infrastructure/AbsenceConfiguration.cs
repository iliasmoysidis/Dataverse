using Backend.Modules.Absences.Domain;
using Backend.Modules.Users.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Modules.Absences.Infrastructure;

public sealed class AbsenceConfiguration : IEntityTypeConfiguration<Absence>
{
    public void Configure(EntityTypeBuilder<Absence> builder)
    {
        builder.ToTable("absences", table =>
        {
            var allowedStatuses = string.Join(",", Enum.GetValues<Status>().Cast<int>());

            table.HasCheckConstraint(
                "CK_absences_status",
                $"status in ({allowedStatuses})"
            );
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");

        builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();

        builder.Property(x => x.StartDate).HasColumnName("start_date").IsRequired();

        builder.Property(x => x.EndDate).HasColumnName("end_date").IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .HasColumnName("status")
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
