namespace Chinchillada.PCGraph.Editor
{
    using System.Collections.Generic;
    using GraphProcessor;
    using UnityEngine.UIElements;

    public abstract class GeneratorNodeView<TNode, TResult> : BaseNodeView where TNode : GeneratorNode<TResult>
    {
        public override void Enable()
        {
            var node = this.GetTargetNode();

            this.SetupControls();

            node.Generated += this.UpdatePreview;

            base.Enable();
        }

        public override void Disable()
        {
            var node = this.GetTargetNode();
            node.Generated -= this.UpdatePreview;

            base.Disable();
        }

        protected abstract IEnumerable<VisualElement> CreateControls();

        protected abstract void UpdatePreview(TResult nodeResult);
        
        private void SetupControls()
        {
            var controls = this.CreateControls();

            foreach (var control in controls)
                this.controlsContainer.Add(control);
        }

        private TNode GetTargetNode() => (TNode) this.nodeTarget;
    }
}