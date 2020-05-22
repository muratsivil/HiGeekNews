﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiGeekNews.Entity.Entities
{
    public class Like : BaseEntity
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
