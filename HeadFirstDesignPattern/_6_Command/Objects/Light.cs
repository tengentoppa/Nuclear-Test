using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadFirstDesignPattern._6_Command.Objects
{
    class Light
    {
        string Position;
        public Light(string pos) { Position = pos; }
        public void On() { Console.WriteLine($"{Position} light is on"); }
        public void Off() { Console.WriteLine($"{Position} light is off"); }
    }
}
