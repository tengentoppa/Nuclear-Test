using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeadFirstDesignPattern._6_Command.Template;

namespace HeadFirstDesignPattern._6_Command.Controls
{
    class NoCommand : ICommand
    {
        public void Excute() { Console.WriteLine("只是個NPC，毫無回應"); }
        public void Undo() { Console.WriteLine("只是個反NPC，毫無回應"); }
    }
}
