using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketClient
{
    public class Client
    {
        //string IPadressString = "192.168.20.147";
        string IPadressString = "localhost";
        private TcpClient klient;
        
        private string messageFromServer;
        private string messageToServer;
        bool _running = true;
        public Client()
        {

            klient = new TcpClient(IPadressString, 20001);
            NetworkStream stream = klient.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            writer.AutoFlush = true;
        }



        public void Run()
        {
           

                                  
            // SEND BESKED TIL SERVER
            while (_running)
            {
                Console.Write("Message til Server?: ");
                string message = Console.ReadLine();
                SendMessage(message);
                

                if (message == "STOP")
                {
                    _running = false;
                   
                }

            }

            // HUSK AT LUKKE WRITER, READER, STREAM OG SOCKET
            reader.Close();
            writer.Close();
            stream.Close();
            klient.Close();

            Console.ReadKey();

        }
        public void StartRecieving()
        {
            Thread recieveThread = new Thread(ReadFromServer);
            Thread sendThread = new Thread(Run);
            recieveThread.Start();
            sendThread.Start();
        }

        public void ReadFromServer()
        {
           

            while (_running)
            {
                messageFromServer = reader.ReadLine();
                
                Console.WriteLine(messageFromServer);
            }

        }
        public void SendMessage(string message)
        {
            writer.WriteLine(message);
            writer.Flush();
        }
        
    }
}
