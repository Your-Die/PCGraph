using UnityEngine;

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
        private PCGraph       graph;
        private BaseGraphView graphView;

        private EditorCoroutine refreshRoutine;

        private static FloatField durationPerNode;

        private Image preview;
        
        protected override void Initialize(BaseGraphView view)
        {
            this.title = "Process Panel";

            var pcgGraphView = (PCGraphView)view;
            this.graph = pcgGraphView.Graph;
            this.graphView = view;

            foreach (var element in this.BuildElements())
                this.content.Add(element);
        }

        private IEnumerable<VisualElement> BuildElements()
        {
            yield return new Button(this.Refresh) { name         = "ActionButton", text = "Refresh" };
            yield return new Button(this.RefreshStepwise) { name = "StepButton", text   = "Refresh Stepwise" };

            yield return durationPerNode = new FloatField("Step duration") { value = 0.8f };
            yield return this.preview = new Image();
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
                    UpdatePreview(processor.CurrentNode);
                    
                    if (stepDuration > 0)
                        yield return new EditorWaitForSeconds(stepDuration);
                    else
                        yield return null;
                }
            }
        }

        private void UpdatePreview(BaseNode node)
        {
            if (!this.graphView.nodeViewsPerNode.TryGetValue(node, out BaseNodeView view))
            {
                Debug.LogWarning($"No view found for active node: {node.name}");
                return;
            }

            if (view is IHasPreviewTexture nodeWithPreview)
            {
                this.preview.image = nodeWithPreview.Preview;
            }
        }
    }
}