using UnityEngine;

namespace Chinchillada.PCGraphs.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using GraphProcessor;
    using Unity.EditorCoroutines.Editor;
    using UnityEngine.UIElements;

    public class PCGraphProcessorView : PinnedElementView
    {
        private PCGraph       graph;
        private BaseGraphView graphView;

        private EditorCoroutine refreshRoutine;

        private static FloatField durationPerNode;

        private Image preview;

        private ManualGraphProcessor manualProcessor;
        
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
            yield return new Button(this.StartManual) { name = "StartManualButton", text   = "Start Manual" };
            yield return new Button(this.StepManual) { name = "StepManualButton", text   = "Step Manual" };

            yield return durationPerNode = new FloatField("Step duration") { value = 0.4f };
            yield return durationPerNode = new FloatField("Manual: iterations per step") { value = 1 };
            yield return this.preview = new Image();
        }



        private void Refresh()
        {
            this.StopRoutine();

            var processor = new PCGraphProcessor(this.graph);

            processor.UpdateComputeOrder();
            processor.Run();
        }

        private void RefreshStepwise()
        {
            var processor = new AsyncGraphProcessor(this.graph, durationPerNode.value);

            this.StopRoutine();

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

        private void StartManual()
        {
            this.StopRoutine();
            
            this.manualProcessor = new ManualGraphProcessor(this.graph);
            this.manualProcessor.Reset();
            
            this.manualProcessor.MoveNext();
        }
        
        private void StepManual()
        {
            this.manualProcessor.MoveNext();
            this.UpdatePreview(this.manualProcessor.CurrentNode);
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
        
        private void StopRoutine()
        {
            if (this.refreshRoutine != null)
                EditorCoroutineUtility.StopCoroutine(this.refreshRoutine);
        }
    }
}