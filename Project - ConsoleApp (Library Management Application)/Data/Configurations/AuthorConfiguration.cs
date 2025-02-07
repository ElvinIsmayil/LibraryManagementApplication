using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project___ConsoleApp__Library_Management_Application_.Entities;

namespace Project___ConsoleApp__Library_Management_Application_.Data.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

        }
    }


}
