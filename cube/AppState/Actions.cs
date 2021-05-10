using BlazorState;

namespace Celin
{
    public partial class AppState
    {
        public class ToggleVersionDisplayAction : IAction
        {
            public F983051.Row Version { get; set; }
        }
        public class SelectUBEAction : IAction
        {
            public F9860.Row UBE { get; set; }
        }
    }
}
