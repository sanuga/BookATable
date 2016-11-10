using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entites;
using DAL.Repositories;

namespace BookATable.Service
{
    public class AuthenticationService
    {
        public  User LoggedUser { get; set; }
        public void Authenticate(string email, string password)
        {
            UsersRepository repo = new UsersRepository();
            LoggedUser = repo.Get(u => u.Email == email && u.Password == password);
        }
    }
}
