using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;
using Switcha.Logic;
using Switcha.Processor.PeerConnection;

namespace Switcha.Processor 
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to SWITCHA!!!");

            Start();

            while (true)
            {
                Console.WriteLine("Switch started, nodes Listening.......");
                Console.WriteLine("Press 1 to Shutdown");
                Console.WriteLine("Press 2 to Restart");
                Console.WriteLine("Press 3 to Exit");

                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        Shutdown();
                        break;

                    case "2":
                        Shutdown();
                        Start();
                        break;
                    case "3":
                        Shutdown();
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Wrong input");
                        Start();
                        break;
                }
            }
        }

        public static void Start()
        {
            List<SourceNode> activeSourceNodes = new SuperEntityLogic<SourceNode>().GetAll().Where(x => x.Status == Status.Active).ToList();

            foreach (SourceNode sourceNode in activeSourceNodes)
            {
                new Listener().StartListener(sourceNode);
            }
        }

        public static void Shutdown()
        {

        }
    }
}
