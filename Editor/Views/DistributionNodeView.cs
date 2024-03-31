using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada.PCGraphs.Editor
{
    public abstract class
        DistributionNodeView<TNode, TValue> : GeneratorNodeViewWithTexture<TNode, IDistribution<TValue>>
        where TNode : GeneratorNode<IDistribution<TValue>>
    {
        private static readonly Color Color = Color.black;

        private const int SampleCount = 500;
        private const int BucketCount = 8;

        private const int BucketWidth = 30;
        private const int MaxHeight   = 100;
        private const int Padding     = 10;
        private const int Spacing     = 10;

        private IRange<TValue> sampleRange;

        private const int TextureWidth = BucketCount * BucketWidth +
                                         (BucketCount - 1) * Spacing +
                                         2 * Padding;

        private const int TextureHeight = MaxHeight + 2 * Padding;

        protected override string Stringify()
        {
            return $"{this.sampleRange} {base.Stringify()}";
        }
        
        protected override Texture2D GenerateTexture(IDistribution<TValue> distribution)
        {
            var histogram = this.GenerateHistogram(distribution);
            return DrawHistogram(histogram);
        }
        
        protected abstract float InverseLerp(TValue min, TValue max, TValue sample);

        protected abstract IRange<TValue> CreateRange(TValue[] samples);

        private Dictionary<int, int> GenerateHistogram(IDistribution<TValue> distribution)
        {
            var samples = distribution.Samples(UnityRandom.Shared)
                                      .Take(SampleCount)
                                      .OrderBy(sample => sample)
                                      .ToArray();

            this.sampleRange = this.CreateRange(samples);
            TValue min = samples.First();
            TValue max = samples.Last();

            return samples.ToHistogram(GetBucket);

            int GetBucket(TValue sample)
            {
                float percentage = this.InverseLerp(min, max, sample);
                return Mathf.FloorToInt(percentage * BucketCount);
            }
        }
        
        
        private static Texture2D DrawHistogram(Dictionary<int, int> histogram)
        {
            var texture = new Texture2D(TextureWidth, TextureHeight);

            var maxCount = histogram.Values.Max();

            var keys = histogram.Keys.OrderBy(key => key);

            int index = 0;
            foreach (var key in keys)
            {
                var count = histogram[key];

                var percentage = (float)count / maxCount;
                var height = Mathf.FloorToInt(percentage * MaxHeight);

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
    }
}