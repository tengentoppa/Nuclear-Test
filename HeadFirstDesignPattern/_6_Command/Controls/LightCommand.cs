using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeadFirstDesignPattern._6_Command.Objects;
using HeadFirstDesignPattern._6_Command.Template;

namespace HeadFirstDesignPattern._6_Command.Controls
{
    class LightOnCommand : ICommand
    {
        Light Light;
        public LightOnCommand(Light light)
        {
            Light = light;
        }

        public void Excute()
        {
            Light.On();
        }

        public void Undo()
        {
            Light.Off();
        }
    }
    class LightOffCommand : ICommand
    {
        Light Light;
        public LightOffCommand(Light light)
        {
            Light = light;
        }

        public void Excute()
        {
            Light.Off();
        }

        public void Undo()
        {
            Light.On();
        }
    }
}
