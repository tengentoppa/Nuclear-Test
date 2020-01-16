using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadFirstDesignPattern._4_Factory.Template
{
    public abstract class Dough { public string Name => GetType().Name; }
    public abstract class Sauce { public string Name => GetType().Name; }
    public abstract class Cheese { public string Name => GetType().Name; }
    public abstract class Veggie { public string Name => GetType().Name; }
    public abstract class Pepperoni { public string Name => GetType().Name; }
    public abstract class Clam { public string Name => GetType().Name; }
}
