using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class Client
    {
        public string ClientRegister(string serverIP)
        {
            byte[] bytes = new byte[1024];

            // Connect to a remote device.  
            // Establish the remote endpoint for the socket.  
            // This example uses port 11000 on the local computer.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse(serverIP);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
    
            Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.  

            Console.WriteLine("Reached here");
            sender.Connect(remoteEP);
            // Encode the data string into a byte array.  
            Console.WriteLine("Reached here");
            byte[] msg = Encoding.ASCII.GetBytes("reqrreg");

            // Send the data through the socket.  
            int bytesSent = sender.Send(msg);

            // Receive the response from the remote device.  
            int bytesRec = sender.Receive(bytes);
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
            return Encoding.ASCII.GetString(bytes, 0, bytesRec);



        }
    }
}
