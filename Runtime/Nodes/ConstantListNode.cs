using System;
using System.Collections.Generic;
using Chinchillada.PCGraphs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.PCGraph.Nodes
{
    [Serializable]
    public class ConstantListNode<T> : GeneratorNode<List<T>>
    {
        [SerializeField, ShowInInspector] public List<T> items;

        public override List<T> Generate()
        {
            return this.items;
        }
    }
}