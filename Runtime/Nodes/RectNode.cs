namespace Chinchillada.PCGraph
{
    using System;
    using GraphProcessor;
    using UnityEngine;

    [Serializable, NodeMenuItem("Primitive/Rect")]
    public class RectNode : RectGeneratorNode
    {
        [Input, ShowAsDrawer] public Vector2 origin; 
        [Input, ShowAsDrawer] public Vector2 size; 
        
        public override Rect Generate()
        {
            return new Rect(this.origin, this.size);
        }
    }
}