namespace Chinchillada.PCGraphs.Editor
{
    using System.Linq;
    using Datastructures;
    using GraphProcessor;
    using JetBrains.Annotations;
    using UnityEngine;

    [UsedImplicitly, NodeCustomEditor(typeof(FloatDistributionNode))]
    public class FloatDistributionNodeView : GeneratorNodeViewWithTexture<FloatDistributionNode, IDistribution<float>>
    {
        private static readonly Color Color     = Color.black;
        private static readonly int   thickness = 1;

        private const float PaddingPercentage = 0.1f;

        private Range sampleRange;

        protected override Texture2D GenerateTexture(IDistribution<float> distribution)
        {
            // Get empty texture.
            var texture = this.GetEmptyTexture();

            // Generate 1 sample for each pixel column.
            var sampleSize = texture.width;
            var samples = distribution.Samples(UnityRandom.Shared)
                                      .Take(sampleSize)
                                      .OrderBy(sample => sample)
                                      .ToArray();


            // Get the valeu range of the samples.
            this.sampleRange = new Range(samples.First(), samples.Last());
            this.sampleRange.GrowWithPercentage(PaddingPercentage);

            // Create a mapper from the sample range to the texture.
            var textureHeightRange = new RangeInt(0, texture.height);
            var mapper             = new FloatToIntMapper(this.sampleRange, textureHeightRange);

            for (var x = 0; x < samples.Length; x++)
            {
                // Get pixel height of the sample.
                var sample      = samples[x];
                var pixelHeight = mapper.Map(sample);

                // Draw a line centered at the sampled height.
                DrawVerticalLine(texture, x, pixelHeight);
            }

            texture.Apply();
            return texture;
        }

        private static void DrawVerticalLine(Texture2D texture, int x, int centerY)
        {
            var startHeight = centerY - thickness / 2;
            var endHeight   = centerY + thickness / 2;

            for (var y = startHeight; y <= endHeight; y++)
                texture.SetPixel(x, y, Color);
        }

        protected override string Stringify()
        {
            return $"{this.sampleRange} {base.Stringify()}";
        }
    }
}