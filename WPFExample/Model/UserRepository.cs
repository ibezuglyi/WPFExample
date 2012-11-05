using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFExample.Model
{
    public class UserRepository
    {

        const string password = "abc";
        const string login = "a";

        private static UserRepository instance;
        public static UserRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserRepository();
                return instance;
            }
        }

        public User GetUserByLoginAndPassword(string firstname, string passowrd)
        {

            // request to db

            if (firstname == login && passowrd == password)
                return new User()
                {
                    FirstName = firstname,
                    LastName = "Ivanov",
                    Id = 1,
                    Password = password
                };

            return null;
        }

    }
}
