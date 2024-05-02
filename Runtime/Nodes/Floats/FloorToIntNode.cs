using System;
using Chinchillada.PCGraphs;
using GraphProcessor;
using UnityEngine;

namespace Chinchillada.PCGraph.Nodes.Floats
{
    [Serializable, NodeMenuItem("Floats/Floor To Int")]
    public class FloorToIntNode : GeneratorNode<int>
    {
        [Input] public float floatValue;
        
        public override int Generate() => Mathf.FloorToInt(this.floatValue);
    }
}