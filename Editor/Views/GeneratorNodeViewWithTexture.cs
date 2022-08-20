namespace Chinchillada.PCGraph.Editor
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public abstract class GeneratorNodeViewWithTexture<TNode, TResult> : BaseGeneratorNodeView<TNode, TResult>
        where TNode : GeneratorNode<TResult>
    {
        private const int MinResolution = 100;

        private Image previewImage;
        
        protected override IEnumerable<VisualElement> CreateControls()
        {
            yield return this.previewImage = new Image();
        }

        protected override void UpdatePreview(TResult nodeResult)
        {
            var texture = this.GenerateTexture(nodeResult);
            this.previewImage.image = EnsureResolution(texture);
        }

        protected abstract Texture2D GenerateTexture(TResult result);
        
        private static Texture2D EnsureResolution(Texture2D texture)
        {
            var smallestDimension = Mathf.Min(texture.width, texture.height);

            var upscale = MinResolution / smallestDimension;

            return upscale <= 1
                ? texture
                : texture.Upscale(upscale);
        }
    }
}