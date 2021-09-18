#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace BasicTeleportationUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            MCM.openGame(); // Open game up inside of MCM class
            MCM.openWindowHost();

            Game.position = new Vector3(100, 32, 100);
            // teleport to position 100,32,100 also i recommand looking at how this works
            // in the game class as its alot more complex then it looks lmao

            while (true) // infinite loop
            {
            }
        }
    }
}
