using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.Context;
using System.Diagnostics;
using Microsoft.Identity.Client;

namespace Data.Services
{
    public class UserService
    {
        public static User Find(string login, string password)
        {
            using (var context = new SQLServerDBContext())
            {
                return context.Users.Where(u => u.Password == password && u.Login == login).FirstOrDefault();
            }
        }

        public static void AddUser(string lastName, string firstName, string middleName, string login, string password)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                LastName = lastName,
                FirstName = firstName,
                MiddleName = middleName,
                Login = login,
                IsDeleted = false,
                Password = password
            };
            using (var context = new SQLServerDBContext())
            {
                try
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                Debug.WriteLine("Пользователь добавлен");
            }
        }

        public static void UpdateUser(User user, string lastName, string firstName, string middleName, string login, string password)
        {
            user.LastName = lastName;
            user.FirstName = firstName;
            user.MiddleName = middleName;
            user.Login = login;
            user.Password = password;
            using (var context = new SQLServerDBContext())
            {
                try
                {
                    context.Users.Update(user);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            Debug.WriteLine("Пользователь обновлен!");
        }

        public static void DeleteUser(User user)
        {
            using (var context = new SQLServerDBContext())
            {
                try
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            Debug.WriteLine("Пользователь удален!");
        }

        public static void HideUser(User user)
        {
            user.IsDeleted = true;
            using (var context = new SQLServerDBContext())
            {
                context.Users.Update(user);
                context.SaveChanges();
            }
            Debug.WriteLine("Пользователь скрыт!");
        }

    }


}
