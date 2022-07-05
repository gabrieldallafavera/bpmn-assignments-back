using Api.Database.Entities.People;

namespace Api.Database.Configurations.People
{
    public class TokenFunctionConfiguration : BaseEntityConfiguration<TokenFunction>
    {
        public override void Configure(EntityTypeBuilder<TokenFunction> builder)
        {
            base.Configure(builder);

            builder
                .Property(x => x.Token)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder
                .Property(x => x.Expires)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .HasOne(x => x.User)
                .WithMany(c => c.TokenFunction)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
