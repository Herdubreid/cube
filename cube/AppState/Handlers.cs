using BlazorState;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Celin
{
    public partial class AppState
    {
        public class ToggleVersionDisplayHandler : ActionHandler<ToggleVersionDisplayAction>
        {
            E1Service E1 { get; }
            AppState State => Store.GetState<AppState>();
            public override async Task<Unit> Handle(ToggleVersionDisplayAction aAction, CancellationToken aCancellationToken)
            {
                var vers = State.VersionList.Single(v => v.F983051_VERS.Equals(aAction.Version.F983051_VERS));
                vers.Selected = !vers.Selected;

                if (vers.Selected)
                {
                    try
                    {
                        var rs = await E1.RequestAsync(new AIS.DiscoverUBERequest
                        {
                            reportName = vers.F983051_PID,
                            reportVersion = vers.F983051_VERS
                        });
                        State.VersionDiscoveryList = State.VersionDiscoveryList.Append(rs);
                    }
                    catch { }
                }
                else
                {
                    State.VersionDiscoveryList = State.VersionDiscoveryList.Where(v => !v.reportVersion.Equals(vers.F983051_VERS));
                }

                State.StateChanged();

                return Unit.Value;
            }
            public ToggleVersionDisplayHandler(IStore store, E1Service e1) : base(store)
            {
                E1 = e1;
            }
        }
        public class SelectUBEHandler : ActionHandler<SelectUBEAction>
        {
            E1Service E1 { get; }
            AppState State => Store.GetState<AppState>();
            public override async Task<Unit> Handle(SelectUBEAction aAction, CancellationToken aCancellationToken)
            {
                State.UBE = aAction.UBE;

                try
                {
                    var rs = await E1.RequestAsync<F983051.Response>(new F983051.Request(aAction.UBE.F9860_OBNM));
                    State.VersionList = rs.fs_DATABROWSE_F983051.data.gridData.rowset;
                    if (rs.fs_DATABROWSE_F983051.data.gridData.summary.records > 0)
                    {
                        State.VersionDiscoveryTemplate = await E1.RequestAsync(new AIS.DiscoverUBERequest
                        {
                            reportName = rs.fs_DATABROWSE_F983051.data.gridData.rowset[0].F983051_PID,
                            reportVersion = rs.fs_DATABROWSE_F983051.data.gridData.rowset[0].F983051_VERS
                        });
                    }
                    State.VersionDiscoveryList = Enumerable.Empty<AIS.DiscoveryUBEResponse>();
                    State.StateChanged();
                }
                catch { }

                return Unit.Value;
            }
            public SelectUBEHandler(IStore store, E1Service e1) : base(store)
            {
                E1 = e1;
            }
        }
    }
}
