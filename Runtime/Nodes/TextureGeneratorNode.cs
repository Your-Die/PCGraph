namespace Chinchillada.PCGraphs.Noise
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public abstract class TextureGeneratorNode : AsyncGeneratorNode<Texture2D>
    {
        public override Texture2D Generate()
        {
            var texture = this.GenerateTextureAsync().Last();
            texture.Apply();

            return texture;
        }

        protected override IEnumerable<Texture2D> GenerateAsync()
        {
            foreach (var texture in this.GenerateTextureAsync())
            {
                texture.Apply();
                yield return texture;
            }
        }

        protected abstract IEnumerable<Texture2D> GenerateTextureAsync();
    }
}