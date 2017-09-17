namespace RThomaz.Application.Financeiro.Helpers.JsTree
{
    public class State
    {
        public State()
        {
            opened = true;
            disabled = false;
            selected = false;
        }
        // is the node open
        public bool opened { get; set; }

        // is the node disabled
        public bool disabled { get; set; }

        // is the node selected
        public bool selected { get; set; }
    }
}