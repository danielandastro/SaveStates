using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to SaveStates Server");
            TokenManager.token = TokenGenerator.Generate(32);
            Console.WriteLine("Here is your token " + TokenManager.token);
            TokenManager.AddToken(TokenManager.token);
            var check = Console.ReadLine();
            if (TokenManager.TokenVerifier(check))
            {
                Console.WriteLine("yaaas");
            }
            else Console.WriteLine("naa");

        }
    }
}
