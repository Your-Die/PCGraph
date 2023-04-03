using System;
using GraphProcessor;
using UnityEngine;

namespace Chinchillada.PCGraphs
{
    [Serializable, NodeMenuItem("Vectors/Vector2")]
    public class Vector2Node : GeneratorNode<Vector2>
    {
        [Input, ShowAsDrawer] public float x;
        [Input, ShowAsDrawer] public float y;
        
        public override Vector2 Generate()
        {
            return new Vector2(this.x, this.y);
        }
    }
}