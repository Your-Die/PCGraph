namespace Chinchillada.PCGraphs.Nodes.Floats
{
    using System;
    using GraphProcessor;
    using UnityEngine;

    [Serializable]
    public abstract class FloatArithmeticNode : GeneratorNode<float>
    {
        [SerializeField, Input, ShowAsDrawer] public float a;
        [SerializeField, Input, ShowAsDrawer] public float b;

        public override float Generate()
        {
            return this.Calculate(this.a, this.b);
        }

        public abstract float Calculate(float x, float y);
    }
}