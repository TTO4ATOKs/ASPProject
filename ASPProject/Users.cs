using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.IO;

namespace ASPProject
{
    public class Users
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Users.txt");

        private int Id = 0;



        public bool TakeInfo(string login, string password)
        {
            if (!File.Exists(_filePath))
                using (File.Create(_filePath)) { }
            string str;
            if ((str = File.ReadAllText(_filePath)) != null) 
            {
                string[] lines = File.ReadAllLines(_filePath);
                string[,] infoUsers = new string[lines.Length, 3];
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] buffer = lines[i].Split(';');
                    for (int j = 0; j < buffer.Length; j++)
                    {
                        infoUsers[i, j] = buffer[j].Trim();
                    }
                }

                for (int i = 0; i < infoUsers.GetLength(0); i++)
                {
                    if (login == infoUsers[i, 1])
                        return true;
                }
            }

            return false;
        }

        public void Register(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                return;

            if (TakeInfo(login,password) == false)
            {
                string info = Id +";" + login + ";" + password;
                File.AppendAllText(_filePath, info + "\n");
                Id++;
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
            string str;
            if ((str = File.ReadAllText(_filePath)) != null)
            {
                string[] lines = File.ReadAllLines(_filePath);
                string[,] infoUsers = new string[lines.Length, 3];
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] buffer = lines[i].Split(';');
                    for (int j = 0; j < buffer.Length; j++)
                    {
                        infoUsers[i, j] = buffer[j].Trim();
                    }
                }

                for (int i = 0; i < infoUsers.GetLength(0); i++)
                {
                    if (login == infoUsers[i, 1])
                        if (password == infoUsers[i,2])
                            return true;
                }
            }

            return false;
        }
    }
}
