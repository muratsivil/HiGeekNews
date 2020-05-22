using HiGeekNews.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiGeekNews.Map.Mapping
{
    public class AppUserMap : CoreMap<AppUser>
    {
        public AppUserMap()
        {
            ToTable("dbo.AppUsers");
            Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            Property(x => x.LastName).HasMaxLength(50).IsRequired();
            Property(x => x.UserName).HasMaxLength(50).IsRequired();
            Property(x => x.Email).HasMaxLength(50).IsRequired();
            Property(x => x.PhoneNumber).HasMaxLength(15).IsOptional();
            Property(x => x.Password).HasMaxLength(50).IsRequired();
            Property(x => x.Role).IsRequired();

            HasMany(x => x.Posts)
                .WithRequired(x => x.AppUser)
                .HasForeignKey(x => x.AppUserId)
                .WillCascadeOnDelete(false);

            HasMany(x => x.Comments)
                .WithRequired(x => x.AppUser)
                .HasForeignKey(x => x.AppUserId)
                .WillCascadeOnDelete(false);

            HasMany(x => x.Likes)
                .WithRequired(x => x.AppUser)
                .HasForeignKey(x => x.AppUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
