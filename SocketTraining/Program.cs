using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            Program myProgram = new Program();
            myProgram.Run();
        }
        private void Run()
        {
            Server sr = new Server("localhost",20000);
            sr.StartServer();

            Console.ReadKey();
        }
    }
}
