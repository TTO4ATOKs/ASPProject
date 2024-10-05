using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.IO;

namespace ASPProject
{
    public struct UserInfo
    {
        public int Id { get; }
        public string Login { get; }
        public string Password { get; }

        public UserInfo(int id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            return $"{Id};{Login};{Password}";
        }

        public static UserInfo FromString(string userData)
        {
            var parts = userData.Split(';');
            if (parts.Length == 3)
            {
                var login = parts[1].Trim();
                var password = parts[2].Trim();
                var id = int.Parse(parts[0].Trim());
                return new UserInfo(id, login, password);
            }
            throw new FormatException("Неправильный формат данных пользователя.");
        }
    }

    public class UsersAuthorization
    {
        private readonly string _filePath = "Users.txt";
        private int Id = 0;

        private List<UserInfo> LoadUsers()
        {
            var users = new List<UserInfo>();

            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Dispose();
                return users;
            }

            using (var stream = new StreamReader(_filePath))
            {
                string? line;
                while ((line = stream.ReadLine()) != null)
                {
                    try { 
                        var user = UserInfo.FromString(line);
                        users.Add(user);
                    }
                    catch (FormatException)
                    { }
                }
            }

            return users;
        }

        private void SaveUser(UserInfo user)
        {
            using (var writer = new StreamWriter(_filePath, append: true))
            {
                writer.WriteLine(user.ToString());
            }
        }

        public bool TakeInfo(string login, string password)
        {
            var users = LoadUsers();
            foreach (var user in users)
            {
                if (user.Login == login)
                {
                    return true;
                }
            }
            return false;
        }

        public void Register(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                return;

            if (!TakeInfo(login, password))
            {
                var users = LoadUsers();
                Id = users.Count > 0 ? users[^1].Id + 1 : 1;
                var newUser = new UserInfo(Id, login, password);
                SaveUser(newUser);
            }
        }

        public bool Login(string login, string password)
        {
            if (login == null || password == null)
                return false;

            return CheckBoolean(login, password);
        }

        public bool CheckBoolean(string login, string password)
        {
            var users = LoadUsers();
            foreach (var user in users)
            {
                if (user.Login == login && user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
