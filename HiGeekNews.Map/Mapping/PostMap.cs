using HiGeekNews.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiGeekNews.Map.Mapping
{
    public class PostMap: CoreMap<Post>
    {
        public PostMap()
        {
            ToTable("dbo.Posts");
            Property(x => x.Content).IsRequired();
            Property(x => x.Header).IsRequired();

            HasRequired(x => x.Category)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.CategoryId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.AppUser)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.AppUserId)
                .WillCascadeOnDelete(false);

            HasMany(x => x.Comments)
                .WithRequired(x => x.Post)
                .HasForeignKey(x => x.PostId)
                .WillCascadeOnDelete(false);

            HasMany(x => x.Likes)
                .WithRequired(x => x.Post)
                .HasForeignKey(x => x.PostId)
                .WillCascadeOnDelete(false);

        }
    }
}
