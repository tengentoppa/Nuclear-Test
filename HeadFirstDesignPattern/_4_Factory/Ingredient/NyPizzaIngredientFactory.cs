using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeadFirstDesignPattern._4_Factory.Template;

namespace HeadFirstDesignPattern._4_Factory.Ingredient
{
    class NyPizzaIngredientFactory : IPizzaIngredientFactory
    {
        public Dough CreateDough()
        {
            return new ThinCrustDough();
        }

        public Sauce CreateSauce()
        {
            return new MarinarraSauce();
        }

        public Cheese CreateCheese()
        {
            return new ReggianoCheese();
        }

        public Veggie[] CreateVeggies()
        {
            return new Veggie[] { new Garlic(), new Onion(), new Mushroom(), new RedPepper() };
        }

        public Pepperoni CreatePepperoni()
        {
            return new SlicedPepperoni();
        }

        public Clam CreateClam()
        {
            return new FreshClams();
        }
    }
}
