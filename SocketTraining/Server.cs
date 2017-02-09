using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace SocketTraining
{
    class Server
    {
        private TcpListener _listener;
        private bool _stopped = false;   

        //private Socket _clientSocket;
        //private NetworkStream stream;
        //private StreamWriter writer;
        //private StreamReader reader;

       public Server(string ipAdress, int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
 
        }
        public void StartServer()
        {
            // unlimited number of requests.
            // should add "STOP-method"
            while (!_stopped)
            {
                Socket requests = _listener.AcceptSocket();
                ClientHandler ch = new ClientHandler(requests);

                Thread t = new Thread(ch.RunClient);
                t.Start();
            }

        }

       
    }
}
