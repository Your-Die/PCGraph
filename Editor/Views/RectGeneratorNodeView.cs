namespace Chinchillada.PCGraphs.Editor
{
    using PCGraphs;
    using GraphProcessor;
    using JetBrains.Annotations;
    using UnityEngine;

    [UsedImplicitly, NodeCustomEditor(typeof(RectGeneratorNode))]
    public class RectGeneratorNodeView : GeneratorNodeViewWithTexture<RectGeneratorNode, Rect>
    {
        private static readonly Color Color = Color.black;

        private const float PaddingPercentage = 0.1f;

        protected override Texture2D GenerateTexture(Rect rect)
        {
            // Get texture.
            var texture = this.GetEmptyTexture();

            // create a mapper from a padded version of the rect to the texture.
            var paddedRect = new Rect(rect).GrowPercentage(PaddingPercentage);
            var mapper     = new RectToTextureMapper(paddedRect, texture);

            // Get the pixel coordinates for the rect.
            var pixelRect = mapper.MapRect(rect);

            // Draw the rect on the texture.
            texture.DrawRect(pixelRect, Color);
            texture.Apply();

            return texture;
        }
    }
}