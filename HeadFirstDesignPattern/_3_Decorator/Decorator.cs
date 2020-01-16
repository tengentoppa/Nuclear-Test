using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadFirstDesignPattern._3_Decorator
{
    public abstract class CondimentDecorator : Beverage
    {
        private Beverage beverage;
        protected abstract decimal cost { get; }
        protected abstract string description { get; }
        public CondimentDecorator(Beverage beverage) { this.beverage = beverage; }
        public override string GetDescription() { return beverage.GetDescription() + ", " + description; }
        public override decimal Cost => beverage.Cost + cost;

        protected Beverage Beverage { get => beverage; set => beverage = value; }
    }

    public class Mocha : CondimentDecorator
    {
        protected override decimal cost => 0.20m;
        protected override string description => nameof(Mocha);
        public Mocha(Beverage beverage) : base(beverage) { }
    }

    public class Milk : CondimentDecorator
    {
        protected override decimal cost => 0.10m;
        protected override string description => nameof(Milk);
        public Milk(Beverage beverage) : base(beverage) { }
    }

    public class Whip : CondimentDecorator
    {
        protected override decimal cost => 0.10m;
        protected override string description => nameof(Whip);
        public Whip(Beverage beverage) : base(beverage) { }
    }

    public class Soy : CondimentDecorator
    {
        protected override decimal cost => 0.15m;
        protected override string description => nameof(Soy);
        public Soy(Beverage beverage) : base(beverage) { }
    }
}
