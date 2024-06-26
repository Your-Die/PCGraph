namespace Chinchillada.PCGraphs.Editor
{
    using Chinchillada;
    using UnityEngine;
    using UnityEngine.UIElements;

    public abstract class GeneratorNodeViewWithTexture<TNode, TResult> : BaseGeneratorNodeView<TNode, TResult>, 
                                                                         IHasPreviewTexture
        where TNode : GeneratorNode<TResult>
    {
        protected const int MinResolution = 100;

        private Image previewImage;

        public Texture Preview => this.previewImage.image;


        protected override void CreateControls(VisualElement controlContainer)
        {
            base.CreateControls(controlContainer);
            
            this.previewImage = new Image();
            controlContainer.Add(this.previewImage);
        }

        protected override void UpdatePreview(TResult nodeResult)
        {
            var texture = this.GenerateTexture(nodeResult);
            this.previewImage.image = EnsureResolution(texture);
        }

        protected abstract Texture2D GenerateTexture(TResult result);

        protected Texture2D GetEmptyTexture() => new Texture2D(MinResolution, MinResolution);
        
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