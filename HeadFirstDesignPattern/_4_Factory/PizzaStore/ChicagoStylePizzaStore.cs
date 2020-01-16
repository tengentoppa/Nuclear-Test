using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeadFirstDesignPattern._4_Factory.PizzaStyle;
using HeadFirstDesignPattern._4_Factory.Template;
using HeadFirstDesignPattern._4_Factory.Ingredient;

namespace HeadFirstDesignPattern._4_Factory.PizzaStore
{
    public class ChicagoStylePizzaStore : Template.PizzaStore
    {
        protected override Pizza CreatePizza(string type)
        {
            if (string.IsNullOrEmpty(type)) { return null; }
            var ingredientFactory = new ChicagoPizzaIngredientFactory();
            switch (type.ToLower())
            {
                case "cheese": return new CheesePizza(ingredientFactory) { Name = "Chicago Style Cheese Pizza" };
                //case "veggie": return new NyStyleVeggiePizza();
                case "clam": return new ClamPizza(ingredientFactory) { Name = "Chicago Style Clam Pizza" };
                //case "pepperoni": return new NyStylePepperoniPizza();
                default: return null;
            }
        }
    }
}
