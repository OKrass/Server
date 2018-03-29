using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleServer
{
    class Program
    {
        // Set Port
        const int port = 3001;
        static TcpListener listener;
        static void Main(string[] args)
        {
            try
            {
                //Change IP adress if needed
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                listener = new TcpListener(localAddr, port);
                listener.Start();
                Console.WriteLine("Awaiting connection...");

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(client);

                    // Creating new thread for a new client
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
            }
        }
    }
}