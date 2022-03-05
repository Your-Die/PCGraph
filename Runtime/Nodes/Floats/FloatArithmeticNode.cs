namespace Chinchillada.PCGraph.Nodes.Floats
{
    using System;
    using GraphProcessor;
    using UnityEngine;

    [Serializable]
    public abstract class FloatArithmeticNode : FloatGeneratorNode
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