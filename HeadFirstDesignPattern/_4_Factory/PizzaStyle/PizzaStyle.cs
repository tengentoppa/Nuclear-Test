using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeadFirstDesignPattern._4_Factory.Template;

namespace HeadFirstDesignPattern._4_Factory.PizzaStyle
{
    public class CheesePizza : Pizza
    {
        public CheesePizza(IPizzaIngredientFactory ingredientFactory) : base(ingredientFactory) { }
        public override void Prepare()
        {
            Console.WriteLine($"Preparing {Name}");
            Dough = IngredientFactory.CreateDough();
            Sauce = IngredientFactory.CreateSauce();
            Cheese = IngredientFactory.CreateCheese();

            Console.WriteLine($"{nameof(Dough)}: {Dough.Name}");
            Console.WriteLine($"{nameof(Sauce)}: {Sauce.Name}");
            Console.WriteLine($"{nameof(Cheese)}: {Cheese.Name}");
        }
    }
    public class ClamPizza : Pizza
    {
        public ClamPizza(IPizzaIngredientFactory ingredientFactory) : base(ingredientFactory) { }
        public override void Prepare()
        {
            Console.WriteLine($"Preparing {Name}");
            Dough = IngredientFactory.CreateDough();
            Sauce = IngredientFactory.CreateSauce();
            Cheese = IngredientFactory.CreateCheese();
            Clam = IngredientFactory.CreateClam();

            Console.WriteLine($"{nameof(Dough)}: {Dough.Name}");
            Console.WriteLine($"{nameof(Sauce)}: {Sauce.Name}");
            Console.WriteLine($"{nameof(Cheese)}: {Cheese.Name}");
            Console.WriteLine($"{nameof(Clam)}: {Clam.Name}");
        }
    }
}
