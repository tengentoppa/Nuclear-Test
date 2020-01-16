using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadFirstDesignPattern._6_Command.Template
{
    public interface ICommand
    {
        void Excute();
        void Undo();
    }
}
