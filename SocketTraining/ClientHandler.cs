using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketTraining
{
    class ClientHandler
    {
        private Socket _clientSocket;
        private NetworkStream stream;
        private StreamWriter writer;
        private StreamReader reader;

        public ClientHandler(Socket insocket)
        {
            this._clientSocket = insocket;
            this.stream = new NetworkStream(_clientSocket);
            this.writer = new StreamWriter(stream);
            this.reader = new StreamReader(stream);
        }

        public void RunClient()
        {
            if (_clientSocket.Connected)
            {
                sendMessageToClient("Good evening " + _clientSocket.RemoteEndPoint + ". How about a nice game of chess?");

                StartDialog();

                reader.Close();
                writer.Close();
                stream.Close();
            }

            _clientSocket.Close();
        }
        private void StartDialog()
        {
            bool running = true;
            while (running)
            {

                // incoming message
                try
                {

                    string messageFromKlient = getMessageFromClient();

                    if (messageFromKlient != null)
                    {
                        //Do stuff on message
                        Console.WriteLine("Besked fra Client: " + _clientSocket.RemoteEndPoint + " " + messageFromKlient);

                        System.IO.DirectoryInfo dir = new DirectoryInfo(messageFromKlient);

                       

                        // IF kartoteket eksisteretr, then do stuff
                        if (dir.Exists)
                        {
                            writer.WriteLine("NAME <"+dir.CreationTime.ToString() + Environment.NewLine);
                            System.IO.DirectoryInfo[] subDirs = dir.GetDirectories();
                            // LAVER ET ARRAY ud af subdirectories
                            foreach (System.IO.DirectoryInfo dirinfo in subDirs)
                            {
                                writer.WriteLine("NAme:<" + dirinfo.Name + "> EXTENSION:<" + dirinfo.Extension + ">" + Environment.NewLine);
                            }
                            writer.Flush();
                        }
                        else
                        {
                            writer.WriteLine("DIRECTORY DOES NOT EXIST");
                        }

                    }
                }
                catch
                {
                    throw;
                }

            }
        }
        // SEND BESKED TIL CLIENT
        private void sendMessageToClient(string text)
        {
            writer.WriteLine(text);
            writer.Flush();
        }
        // MODTAG BESKED FRA CLIENT
        private string getMessageFromClient()
        {
            try
            {
                return reader.ReadLine();
            }
            catch
            {
                return null;
            }
        }
    }
}





