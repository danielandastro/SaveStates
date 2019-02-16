using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var database = new DBEngine();
            database.DBInit();
            Console.WriteLine("Welcome to SaveStates Server");
            var token = TokenGenerator.GenerateToken(32);
            Console.WriteLine("Here is your token " + token);
            database.AddData(token);
            var check = Console.ReadLine();
            if (database.DBLookup(check))
            {
                Console.WriteLine("yaaas");
            }
            else Console.WriteLine("naa");

        }
    }
}
