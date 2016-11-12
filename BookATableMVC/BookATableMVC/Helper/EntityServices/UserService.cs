using DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BookATableMVC.Services.EntityServices
{
    public class UserService:BaseService<User>
    {
        public User GetByGuid(string guid)
        {
            return GetAll().FirstOrDefault(u => u.Password == guid);
        }
      
    }
    
}