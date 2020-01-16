using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadFirstDesignPattern._4_Factory.Template
{
    public abstract class Pizza
    {
        protected IPizzaIngredientFactory IngredientFactory { get; private set; }
        public string Name { get; set; }
        protected Dough Dough { get; set; }
        protected Sauce Sauce { get; set; }
        protected Veggie[] Veggies { get; set; }
        protected Cheese Cheese { get; set; }
        protected Pepperoni Pepperoni { get; set; }
        protected Clam Clam { get; set; }

        public Pizza(IPizzaIngredientFactory ingredientFactory) { IngredientFactory = ingredientFactory; }

        public abstract void Prepare();
        public virtual void Bake()
        {
            Console.WriteLine("Bake for 25 minutes at 350");
        }
        public virtual void Cut()
        {
            Console.WriteLine("Cutting the pizza into diagonal slices");
        }
        public virtual void Box()
        {
            Console.WriteLine("Place pizza into official PizzaStroe box");
        }
    }
}
