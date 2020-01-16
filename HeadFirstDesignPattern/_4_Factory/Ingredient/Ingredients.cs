using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeadFirstDesignPattern._4_Factory.Template;

namespace HeadFirstDesignPattern._4_Factory.Ingredient
{
    #region Dough
    public class ThinCrustDough : Dough { }
    public class ThickCrustDough : Dough { }
    #endregion

    #region Sauce
    public class MarinarraSauce : Sauce { }
    public class PlumTomatoSauce : Sauce { }
    #endregion

    #region Cheese
    public class ReggianoCheese : Cheese { }
    public class Mozzarella : Cheese { }
    #endregion

    #region Veggie
    public class Garlic : Veggie { }
    public class Onion : Veggie { }
    public class Mushroom : Veggie { }
    public class RedPepper : Veggie { }
    public class EggPlant : Veggie { }
    public class Spinach : Veggie { }
    public class BlackOlives : Veggie { }
    #endregion

    #region Pepperoni
    public class SlicedPepperoni : Pepperoni { }
    #endregion

    #region Clam
    public class FreshClams : Clam { }
    public class FrozenClams : Clam { }
    #endregion
}
