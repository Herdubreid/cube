using BlazorState;
using System;

namespace Celin
{
    public class AppStateComponent : BlazorStateComponent, IDisposable
    {
        protected AppState State => Store.GetState<AppState>();
        protected void Update(object sender, EventArgs args) => InvokeAsync(StateHasChanged);
        protected override void OnInitialized()
        {
            base.OnInitialized();
            State.Changed += Update;
        }
        public new void Dispose()
        {
            State.Changed -= Update;
            base.Dispose();
        }
    }
}
