using BlazorState;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Celin
{
    public partial class AppState : State<AppState>
    {
        public F9860.Row UBE { get; set; }
        public IEnumerable<F983051.Row> VersionList { get; set; }
        public AIS.DiscoveryUBEResponse VersionDiscoveryTemplate { get; set; }
        public IEnumerable<AIS.DiscoveryUBEResponse> VersionDiscoveryList { get; set; }
        public event EventHandler Changed;
        public void StateChanged(EventArgs e = default)
        {
            var handler = Changed;
            handler?.Invoke(this, e);
        }
        public override void Initialize()
        {
            VersionList = Enumerable.Empty<F983051.Row>();
            VersionDiscoveryList = Enumerable.Empty<AIS.DiscoveryUBEResponse>();
        }
    }
}
