using System;
using GraphProcessor;

namespace Chinchillada.PCGraphs
{
    [Serializable, NodeMenuItem("Ints/Distributions/Step Increase")]
    public class StepIncreaseNode : IntDistributionNode
    {
        [Input, ShowAsDrawer] public int   startValue     = 1;
        [Input, ShowAsDrawer] public int   step           = 1;
        [Input, ShowAsDrawer] public float startChance    = 0.5f;
        [Input, ShowAsDrawer] public float   chanceDecrease = 2;

        public override IDistribution<int> Generate()
        {
            return new StepIncreaseDistribution(this.startValue, this.step, this.startChance, this.chanceDecrease);
        }
    }

    public class StepIncreaseDistribution : IDistribution<int>
    {
        private readonly int startValue = 1;
        private readonly int step       = 1;

        private readonly float startChance    = 0.5f;
        private readonly float chanceDecrease = 2;

        public StepIncreaseDistribution(int startValue, int step, float startChance, float chanceDecrease)
        {
            if (startChance <= 0)
                throw new InvalidOperationException($"{nameof(startChance)} needs to be above 0: {startChance}");
            if (chanceDecrease <= 1)
                throw new InvalidOperationException($"{nameof(chanceDecrease)} needs to be above 1: {chanceDecrease}");
            
            this.startValue = startValue;
            this.step = step;
            this.startChance = startChance;
            this.chanceDecrease = chanceDecrease;
        }

        public int Sample(IRNG random)
        {
            int value = this.startValue;
            float chance = this.startChance;

            while (random.Flip(chance))
            {
                value += this.step;
                chance /= this.chanceDecrease;
            }

            return value;
        }
    }
}