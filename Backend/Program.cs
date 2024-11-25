using System;
using Backend;

class Program
{
    static void Main(string[] args)
    {
        var db = new FiilismittariTietokanta();

        while (true)
        {
            Console.WriteLine("Valitse toiminto:");
            Console.WriteLine("1. Lisää käyttäjä");
            Console.WriteLine("2. Näytä kaikki käyttäjät");
            Console.WriteLine("3. Lisää käyttäjän data");
            Console.WriteLine("4. Näytä kaikki käyttäjien datat");
            Console.WriteLine("5. Poistu");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddUser(db);
                    break;
                case "2":
                    ShowUsers(db);
                    break;
                case "3":
                    AddUserData(db);
                    break;
                case "4":
                    ShowUserDatas(db);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Virheellinen valinta, yritä uudelleen.");
                    break;
            }
        }
    }

    static void AddUser(FiilismittariTietokanta db)
    {
        Console.Write("Anna etunimi: ");
        var firstName = Console.ReadLine();

        Console.Write("Anna sukunimi: ");
        var lastName = Console.ReadLine();

        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            Console.WriteLine("Etunimi ja sukunimi eivät voi olla tyhjiä.");
            return;
        }

        db.AddUser(firstName, lastName);
        Console.WriteLine("Käyttäjä lisätty.");
    }

    static void ShowUsers(FiilismittariTietokanta db)
    {
        var users = db.GetAllUsers();
        Console.WriteLine("Users:");
        foreach (var u in users)
        {
            Console.WriteLine($"{u.Id}: {u.FirstName} {u.LastName}");
        }
    }

    static void AddUserData(FiilismittariTietokanta db)
    {
        Console.Write("Anna käyttäjän ID: ");
        var userIdInput = Console.ReadLine();
        if (!int.TryParse(userIdInput, out var userId))
        {
            Console.WriteLine("Virheellinen käyttäjän ID.");
            return;
        }

        Console.Write("Anna päivämäärä (yyyy-mm-dd): ");
        var dateInput = Console.ReadLine();
        if (!DateTime.TryParse(dateInput, out var date))
        {
            Console.WriteLine("Virheellinen päivämäärä.");
            return;
        }

        Console.Write("Anna arvo: ");
        var valueInput = Console.ReadLine();
        if (!int.TryParse(valueInput, out var value))
        {
            Console.WriteLine("Virheellinen arvo.");
            return;
        }

        db.AddUserData(userId, date, value);
        Console.WriteLine("Käyttäjän data lisätty.");
    }

    static void ShowUserDatas(FiilismittariTietokanta db)
    {
        var userDatas = db.GetAllUserDatas();
        Console.WriteLine("User Data:");
        foreach (var ud in userDatas)
        {
            Console.WriteLine($"{ud.Id}: UserId={ud.UserId}, Date={ud.Date}, Value={ud.Value}");
        }
    }
}