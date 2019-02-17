using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            DBEngine.DBInit();
            Console.WriteLine("Welcome to SaveStates Server");


        }
        static void Listener()
        {
            byte[] bytes = new byte[1024];
            string data = null;
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();
                    data = null;

                    // An incoming connection needs to be processed.  
                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }

                    // Show the data on the console.  
                    var dataToSend = ProcessData(data);
                    if (!dataToSend.Equals(""))
                    {
                        byte[] msg = Encoding.ASCII.GetBytes(dataToSend);
                        handler.Send(msg);
                    }
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        static string ProcessData(string dataToProcess)
        {

            if (dataToProcess.Equals("regreq"))
            {
                var tempToken = TokenGenerator.GenerateToken(32);
                DBEngine.AddData(tempToken, "");
                return tempToken;
            }
            else if (dataToProcess.Contains("datareq"))
            {
                var temp = dataToProcess.Split(':')[1];
                return DBEngine.DbLookup(temp);
            }
            else if (dataToProcess.Contains("storereq"))
            {
                var temp = dataToProcess.Split(':');
                DBEngine.AddData(temp[1], temp[2]);
                return "";
            }
            else return "";

        }
    }
}