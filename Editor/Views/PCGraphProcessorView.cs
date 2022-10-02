namespace Chinchillada.PCGraphs.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using GraphProcessor;
    using Unity.EditorCoroutines.Editor;
    using UnityEditor.UIElements;
    using UnityEngine.UIElements;

    public class PCGraphProcessorView : PinnedElementView
    {
        private PCGraph graph;

        private EditorCoroutine refreshRoutine;

        private static FloatField durationPerNode;

        protected override void Initialize(BaseGraphView graphView)
        {
            this.title = "Process Panel";

            var pcgGraphView = (PCGraphView)graphView;
            this.graph = pcgGraphView.Graph;

            foreach (var element in this.BuildElements())
                this.content.Add(element);
        }

        private IEnumerable<VisualElement> BuildElements()
        {
            yield return new Button(this.Refresh) { name         = "ActionButton", text = "Refresh" };
            yield return new Button(this.RefreshStepwise) { name = "StepButton", text   = "Refresh Stepwise" };

            yield return durationPerNode = new FloatField("Step duration") { value = 0.8f };
        }

        private void Refresh()
        {
            var processor = new PCGraphProcessor(this.graph);

            processor.UpdateComputeOrder();
            processor.Run();
        }

        private void RefreshStepwise()
        {
            var processor = new AsyncGraphProcessor(this.graph, durationPerNode.value);

            if (this.refreshRoutine != null)
                EditorCoroutineUtility.StopCoroutine(this.refreshRoutine);

            processor.UpdateComputeOrder();

            this.refreshRoutine = EditorCoroutineUtility.StartCoroutine(Routine(), this);

            IEnumerator Routine()
            {
                var process = processor.RunAsync();

                foreach (var stepDuration in process)
                {
                    if (stepDuration > 0)
                        yield return new EditorWaitForSeconds(stepDuration);
                    else
                        yield return null;
                }
            }
        }
    }
}