using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidProject
{
    internal class Program
    {
        static void Main(string[] args)
        {


            GameManager gameManager = new GameManager();
            gameManager.OptionsMenu();


            Console.ReadKey();
        }
    }
}
