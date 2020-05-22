using HiGeekNews.Entity.Entities;
using HiGeekNews.Service.BaseRepository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiGeekNews.Service.Repositories
{
    public class AppUserRepository : CoreRepository<AppUser>
    {
        public AppUser FindByUserName(string userName)
        {
            return GetByDefault(x => x.UserName == userName);
        }

        public bool CheckCredentials(string userName, string password)
        { 
            return Any(x => x.UserName == userName && x.Password == password);
        }
    }
}

