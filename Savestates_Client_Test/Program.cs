using System;
using Client;
namespace Savestates_Client_Test
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var client = new Client.Client();
            Console.WriteLine(client.ClientRegister("127.0.0.1"));
        }
    }
}
