using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeadFirstDesignPattern._6_Command.Objects;
using HeadFirstDesignPattern._6_Command.Template;

namespace HeadFirstDesignPattern._6_Command.Controls
{
    class CeilingFanOnCommand : ICommand
    {
        Fan Fan;
        public CeilingFanOnCommand(Fan fan)
        {
            Fan = fan;
        }

        public void Excute()
        {
            Fan.On();
        }

        public void Undo()
        {
            Fan.Off();
        }
    }
    class CeilingFanOffCommand : ICommand
    {
        Fan Fan;
        public CeilingFanOffCommand(Fan fan)
        {
            Fan = fan;
        }

        public void Excute()
        {
            Fan.Off();
        }

        public void Undo()
        {
            Fan.On();
        }
    }
}
