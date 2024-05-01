namespace Chinchillada.PCGraphs.Editor
{
    using Chinchillada;
    using GraphProcessor;
    using UnityEngine.UIElements;

    [NodeCustomEditor(typeof(GeneratorNode))]
    public class GeneratorNodeView : BaseNodeView
    {
        private TextField textPreview;

        protected GeneratorNode Node => (GeneratorNode)this.nodeTarget;
        
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
            
            this.CreateControls(this.controlsContainer);
        }

        protected virtual void CreateControls(VisualElement controlContainer)
        {
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
        protected TNode TypedNode => (TNode)this.nodeTarget;
        
        protected override void OnNodeProcessed()
        {
            this.UpdatePreview(this.TypedNode.Result);
            
            base.OnNodeProcessed();
        }

        protected abstract void UpdatePreview(TResult nodeResult);

        protected override void CreateControls(VisualElement controlContainer)
        {
            if (!(this.TypedNode is IIteratorNode<TResult> iteratorNode))
                return;
            
            var button = new Button(() => this.RunIteration(iteratorNode))
            {
                text = "Iterate"
            };
                
            controlContainer.Add(button);
        }

        private void RunIteration(IIteratorNode<TResult> iteratorNode)
        {
            var iteration = iteratorNode.RunIteration();
            UpdatePreview(iteration);
        }
    }
}