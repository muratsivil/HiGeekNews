﻿using HiGeekNews.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiGeekNews.Entity.Entities
{
    
    public class BaseEntity
    {
        public int Id { get; set; }

        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get { return _createDate; } set { _createDate = value; } }

        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get { return _status; } set { _status = value; } }
    }
}
