namespace Chinchillada.PCGraph.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using GraphProcessor;
    using Unity.EditorCoroutines.Editor;
    using UnityEngine.UIElements;

    public class PCGraphProcessorView : PinnedElementView
    {
        private PCGraph graph;

        private EditorCoroutine refreshRoutine;

        private const float StepInterval = 0.2f;

        protected override void Initialize(BaseGraphView graphView)
        {
            this.title = "Process Panel";

            var pcgGraphView = (PCGraphView) graphView;
            this.graph = pcgGraphView.Graph;

            foreach (var element in this.BuildElements())
                this.content.Add(element);
        }

        private IEnumerable<VisualElement> BuildElements()
        {
            yield return new Button(this.Refresh) {name         = "ActionButton", text = "Refresh"};
            yield return new Button(this.RefreshStepwise) {name = "StepButton", text   = "Refresh Stepwise"};
        }

        private void Refresh()
        {
            var processor = new PCGraphProcessor(this.graph);

            processor.UpdateComputeOrder();
            processor.Run();
        }

        private void RefreshStepwise()
        {
            var processor = new AsyncGraphProcessor(this.graph);

            if (this.refreshRoutine != null)
                EditorCoroutineUtility.StopCoroutine(this.refreshRoutine);

            processor.UpdateComputeOrder();

            this.refreshRoutine = EditorCoroutineUtility.StartCoroutine(RefreshRoutine(), this);

            IEnumerator RefreshRoutine()
            {
                var subRoutine = processor.RunAsync();

                while (subRoutine.MoveNext())
                {
                    yield return subRoutine.Current;
                    yield return new EditorWaitForSeconds(StepInterval);
                }
            }
        }
    }
}