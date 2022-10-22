namespace Chinchillada.PCGraph
{
    using System;
    using GraphProcessor;
    using PCGraphs;
    using UnityEngine;

    [Serializable, NodeMenuItem("Vectors/Vector2 Int")]
    public class Vector2IntNode : GeneratorNode<Vector2Int>
    {
        [Input, ShowAsDrawer] public int x;
        [Input, ShowAsDrawer] public int y;
        
        public override Vector2Int Generate()
        {
            return new Vector2Int(this.x, this.y);
        }
    }
}
