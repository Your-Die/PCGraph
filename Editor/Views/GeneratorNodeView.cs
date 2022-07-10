namespace Chinchillada.PCGraph.Editor
{
    using System.Collections.Generic;
    using GraphProcessor;
    using UnityEngine.UIElements;

    [NodeCustomEditor(typeof(GeneratorNode))]
    public class GeneratorNodeView : BaseNodeView
    {
        private TextField textPreview;
        
        private GeneratorNode Node => (GeneratorNode)this.nodeTarget;
        
        protected virtual bool UseTextPreview => true;

        public override void Enable()
        {
            this.SetupControls();

            this.nodeTarget.onProcessed += this.OnNodeProcessed;

            base.Enable();
        }
        
        public override void Disable()
        {
            this.nodeTarget.onProcessed -= this.OnNodeProcessed;
          
            base.Disable();
        }

        private void SetupControls()
        {
            if (this.UseTextPreview)
            {
                this.textPreview = new TextField();
                this.controlsContainer.Add(this.textPreview);
            }
            
            var controls = this.CreateControls();

            foreach (var control in controls)
                this.controlsContainer.Add(control);
        }

        protected virtual IEnumerable<VisualElement> CreateControls()
        {
            yield break;
        }
        
        protected virtual void OnNodeProcessed()
        {
            if (this.UseTextPreview) 
                this.textPreview.value = this.Stringify();
        }

        protected virtual string Stringify()
        {
            return this.Node.ResultObject.ToString();
        }
    }

    public abstract class BaseGeneratorNodeView<TNode, TResult> : GeneratorNodeView 
        where TNode : GeneratorNode<TResult>
    {
        protected override void OnNodeProcessed()
        {
            var node = (TNode)this.nodeTarget;
            this.UpdatePreview(node.Result);
            
            base.OnNodeProcessed();
        }

        protected abstract void UpdatePreview(TResult nodeResult);
    }
}