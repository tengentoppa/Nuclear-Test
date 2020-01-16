using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadFirstDesignPattern._3_Decorator
{
    public abstract class Beverage
    {
        public abstract string GetDescription();
        public abstract decimal Cost { get; }
    }

    public class Espresso : Beverage
    {
        public override string GetDescription()
        {
            return nameof(Espresso);
        }

        public override decimal Cost { get => 1.99m; }
    }

    public class HouseBlend : Beverage
    {
        public override string GetDescription()
        {
            return nameof(HouseBlend);
        }

        public override decimal Cost { get => 0.89m; }
    }

    public class DarkRoast : Beverage
    {
        public override string GetDescription()
        {
            return nameof(DarkRoast);
        }

        public override decimal Cost { get => 0.99m; }
    }

    public class Decaf : Beverage
    {
        public override string GetDescription()
        {
            return nameof(Decaf);
        }

        public override decimal Cost { get => 1.05m; }
    }
}
