using HeadFirstDesignPattern._4_Factory.PizzaStore;
using HeadFirstDesignPattern._4_Factory.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadFirstDesignPattern._4_Factory
{
    class Test
    {
        public static void Run()
        {
            Template.PizzaStore pizzaStore;

            pizzaStore = new NyStylePizzaStore();
            pizzaStore.OrderPizza("Cheese");
            Console.WriteLine(new string('-', 30));
            Console.ReadLine();
            pizzaStore.OrderPizza("clam");

            Console.Write(new string('-', 30));
            Console.ReadLine();

            pizzaStore = new ChicagoStylePizzaStore();
            pizzaStore.OrderPizza("cheese");
            Console.WriteLine(new string('-', 30));
            Console.ReadLine();
            pizzaStore.OrderPizza("clam");
        }
    }
}
