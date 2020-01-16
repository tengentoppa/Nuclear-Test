using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadFirstDesignPattern._3_Decorator
{
    class Test
    {
        public static string Run()
        {
            var result = "";
            Beverage beverage = new Decaf();

            while (true)
            {
                var bar = foo(Console.ReadLine(), beverage);
                if (bar == null) { break; }
                else { beverage = bar; }
            }


            result = $"{beverage.GetDescription()}, Cost: {beverage.Cost}";
            return result;

            CondimentDecorator foo(string input, Beverage bever)
            {
                switch (input)
                {
                    case "w": return new Whip(bever);
                    case "mo": return new Mocha(bever);
                    case "m": return new Milk(bever);
                    case "s": return new Soy(bever);
                    default: return null;
                }
            }
        }
    }
}
