namespace Chinchillada.PCGraphs.Editor
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using GraphProcessor;
    using JetBrains.Annotations;
    using UnityEngine;

    [UsedImplicitly, NodeCustomEditor(typeof(FloatDistributionNode))]
    public class FloatDistributionNodeView : GeneratorNodeViewWithTexture<FloatDistributionNode, IDistribution<float>>
    {
        private static readonly Color Color = Color.black;

        private const int SampleCount = 500;
        private const int BucketCount = 8;

        private const int BucketWidth = 30;
        private const int MaxHeight   = 100;
        private const int Padding     = 10;
        private const int Spacing     = 10;

        private Range sampleRange;

        private const int TextureWidth = BucketCount       * BucketWidth +
                                         (BucketCount - 1) * Spacing     +
                                         2                 * Padding;

        private const int TextureHeight = MaxHeight + 2 * Padding;

        protected override Texture2D GenerateTexture(IDistribution<float> distribution)
        {
            var histogram = this.GenerateHistogram(distribution);
            return this.DrawHistogram(histogram);
        }

        private Dictionary<int, int> GenerateHistogram(IDistribution<float> distribution)
        {
            var samples = distribution.Samples(UnityRandom.Shared)
                                      .Take(SampleCount)
                                      .OrderBy(sample => sample)
                                      .ToArray();

            this.sampleRange = new Range
            {
                Minimum = samples.First(),
                Maximum = samples.Last()
            };

            var min = samples.First();
            var max = samples.Last();

            return samples.ToHistogram(GetBucket);

            int GetBucket(float sample)
            {
                var percentage = Mathf.InverseLerp(min, max, sample);
                return Mathf.FloorToInt(percentage * BucketCount);
            }
        }

        private Texture2D DrawHistogram(Dictionary<int, int> histogram)
        {
            var texture = new Texture2D(TextureWidth, TextureHeight);

            var maxCount = histogram.Values.Max();

            var keys = histogram.Keys.OrderBy(key => key);

            int index = 0;
            foreach (var key in keys)
            {
                var count      = histogram[key];
                
                var percentage = (float)count / maxCount;
                var height     = Mathf.FloorToInt(percentage * MaxHeight);

                var xMin = GetStartX(index);
                var rect = new RectInt(xMin, 0, BucketWidth, height);

                texture.DrawRect(rect, Color);

                index++;
            }

            texture.Apply();
            return texture;

            int GetStartX(int bucketIndex)
            {
                var startX = Padding + bucketIndex * BucketWidth;

                if (bucketIndex > 0)
                {
                    startX += (bucketIndex) * Spacing;
                }

                return startX;
            }
        }

        protected override string Stringify()
        {
            return $"{this.sampleRange} {base.Stringify()}";
        }
    }
}