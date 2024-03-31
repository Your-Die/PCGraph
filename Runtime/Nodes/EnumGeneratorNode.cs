using System;

namespace Chinchillada.PCGraphs.Grid.Ints
{
    public class EnumGeneratorNode<T> : GeneratorNode<T>, IUsesRNG where T : Enum
    {
        public IRNG RNG { get; set; }
        
        public override T Generate()
        {
            var values = EnumHelper.GetValues<T>();
            return values.ChooseRandom(this.RNG);
        }
    }
}