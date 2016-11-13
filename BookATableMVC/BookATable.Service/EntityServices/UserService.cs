using DAL.Entites;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BookATableMVC.Services.EntityServices
{
    public class UserService:BaseService<User>
    {
        public UserService() : base()
        {

        }
        public UserService(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public User GetByGuid(string guid)
        {
            return GetAll().FirstOrDefault(u => u.Password == guid);
        }
      
    }

}