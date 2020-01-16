using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadFirstDesignPattern._6_Command.Objects
{
    class Fan
    {
        string Position;
        public Fan(string pos) { Position = pos; }
        public void On() { Console.WriteLine($"{Position} fan is on"); }
        public void Off() { Console.WriteLine($"{Position} fan is off"); }
    }
}
