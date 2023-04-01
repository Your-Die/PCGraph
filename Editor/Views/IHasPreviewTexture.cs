using UnityEngine;
using UnityEngine.UIElements;

namespace Chinchillada.PCGraphs.Editor
{
    public interface IHasPreviewTexture
    {
        Texture Preview { get; }
    }
}