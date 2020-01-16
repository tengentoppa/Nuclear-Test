using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeadFirstDesignPattern._4_Factory.Template;
using HeadFirstDesignPattern._4_Factory.PizzaStyle;
using HeadFirstDesignPattern._4_Factory.Ingredient;

namespace HeadFirstDesignPattern._4_Factory.PizzaStore
{
    public class NyStylePizzaStore : Template.PizzaStore
    {
        protected override Pizza CreatePizza(string type)
        {
            if (string.IsNullOrEmpty(type)) { return null; }
            var ingredientFactory = new NyPizzaIngredientFactory();
            switch (type.ToLower())
            {
                case "cheese": return new CheesePizza(ingredientFactory) { Name = "New York Style Cheese Pizza"};
                //case "veggie": return new NyStyleVeggiePizza();
                case "clam": return new ClamPizza(ingredientFactory) { Name = "New York Style Clam Pizza" };
                //case "pepperoni": return new NyStylePepperoniPizza();
                default: return null;
            }
        }
    }
}
