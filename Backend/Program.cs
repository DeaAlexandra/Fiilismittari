using BackendProject.Data;
using BackendProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new AppDbContext())
        {
            context.Database.Migrate();

            // Lisää käyttäjä
            var user = new User { FirstName = "John", LastName = "Doe" };
            context.Users.Add(user);
            context.SaveChanges();

            // Lisää käyttäjän data
            var userData = new UserData { UserId = user.Id, Date = DateTime.Now, Value = 100 };
            context.UserDatas.Add(userData);
            context.SaveChanges();

            // Tarkista tiedot
            var users = context.Users.ToList();
            var userDatas = context.UserDatas.ToList();

            Console.WriteLine("Users:");
            foreach (var u in users)
            {
                Console.WriteLine($"{u.Id}: {u.FirstName} {u.LastName}");
            }

            Console.WriteLine("User Data:");
            foreach (var ud in userDatas)
            {
                Console.WriteLine($"{ud.Id}: UserId={ud.UserId}, Date={ud.Date}, Value={ud.Value}");
            }
        }
    }
}