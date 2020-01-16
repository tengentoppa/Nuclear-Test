using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeadFirstDesignPattern._6_Command.Template;
using HeadFirstDesignPattern._6_Command.Controls;

namespace HeadFirstDesignPattern._6_Command
{
    class ControlCenter
    {
        public const int ControlLength = 7;
        ICommand[] OnCommands;
        ICommand[] OffCommands;
        Stack<ICommand> UndoCommands;
        public ControlCenter()
        {
            OnCommands = new ICommand[ControlLength];
            OffCommands = new ICommand[ControlLength];
            UndoCommands = new Stack<ICommand>();

            for (int i = 0; i < ControlLength; i++)
            {
                OnCommands[i] = new NoCommand();
                OffCommands[i] = new NoCommand();
            }
        }

        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand)
        {
            if (slot >= OnCommands.Length) { return; }
            if (slot >= OffCommands.Length) { return; }

            OnCommands[slot] = onCommand;
            OffCommands[slot] = offCommand;
        }

        public void PressOnButton(int slot)
        {
            if (slot >= OnCommands.Length) { return; }
            OnCommands[slot].Excute();
            UndoCommands.Push(OnCommands[slot]);
        }
        public void PressOffButton(int slot)
        {
            if (slot >= OffCommands.Length) { return; }
            OffCommands[slot].Excute();
            UndoCommands.Push(OffCommands[slot]);
        }
        public bool PressUndoButton()
        {
            if (!UndoCommands.Any()) { return false; } // Nothing to undo
            UndoCommands.Pop().Undo();
            return true;
        }

        public override string ToString()
        {
            const int signLen = 7;
            var result = new StringBuilder();

            result.Append($"\n{new string('-', signLen)} Remote Control {new string('-', signLen)}\n");
            for (int i = 0; i < OnCommands.Length; i++)
            {
                result.Append($"[Slot {i}] {OnCommands[i].GetType().Name}  \t{OffCommands[i].GetType().Name}\n");
            }

            return result.ToString();
        }
    }
}
