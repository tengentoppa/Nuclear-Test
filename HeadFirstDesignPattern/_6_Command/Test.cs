using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeadFirstDesignPattern._6_Command.Controls;
using HeadFirstDesignPattern._6_Command.Objects;

namespace HeadFirstDesignPattern._6_Command
{
    class Test
    {
        public static void Run()
        {
            var test = new ControlCenter();

            Console.WriteLine(test);

            Light livingRoomLight = new Light("Living Room");
            Light kitchenLight = new Light("Kitchen Room");
            Fan ceilingFan = new Fan("Living Room");

            var livingRoomLightOn = new LightOnCommand(livingRoomLight);
            var livingRoomLightOff = new LightOffCommand(livingRoomLight);

            var kitchenLightOn = new LightOnCommand(kitchenLight);
            var kitchenLightOff = new LightOffCommand(kitchenLight);

            var ceilingFanOn = new CeilingFanOnCommand(ceilingFan);
            var ceilingFanOff = new CeilingFanOffCommand(ceilingFan);

            test.SetCommand(0, livingRoomLightOn, livingRoomLightOff);
            test.SetCommand(1, kitchenLightOn, kitchenLightOff);
            test.SetCommand(2, ceilingFanOn, ceilingFanOff);

            for (int i = 0; i < ControlCenter.ControlLength; i++)
            {
                test.PressOnButton(i);
                test.PressOffButton(i);
            }
            while (test.PressUndoButton()) { }
        }
    }
}
