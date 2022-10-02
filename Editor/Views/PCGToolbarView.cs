namespace Chinchillada.PCGraphs.Editor
{
    using GraphProcessor;
    using UnityEditor;
    using Status = UnityEngine.UIElements.DropdownMenuAction.Status;

    public class PCGToolbarView : ToolbarView
    {
        public PCGToolbarView(BaseGraphView graphView) : base(graphView)
        {
        }

        protected override void AddButtons()
        {
            this.AddCenterButton();

            this.AddViewToggle<PCGraphProcessorView>("Show Processor");
            this.AddViewToggle<ExposedParameterView>("Show Parameters");

            this.AddShowInProjectButton();
        }

        private void AddCenterButton()
        {
            this.AddButton("Center", this.graphView.ResetPositionAndZoom);
        }

        private void AddShowInProjectButton()
        {
            this.AddButton("Show In Project", PingGraph, false);
            
            void PingGraph() => EditorGUIUtility.PingObject(this.graphView.graph);
        }

        private void AddViewToggle<TView>(string text) where TView : PinnedElementView
        {
            var status    = this.graphView.GetPinnedElementStatus<TView>();
            var isVisible = status != Status.Hidden;

            this.AddToggle(text, isVisible, ToggleView);

            void ToggleView(bool _) => this.graphView.ToggleView<TView>();
        }
    }
}