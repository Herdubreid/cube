using System;
using System.Threading;
using System.Threading.Tasks;

namespace Celin
{
    class Debouncer : IDisposable
    {
        private CancellationTokenSource lastCToken;
        private int milliseconds;
        public Debouncer(int milliseconds = 300)
        {
            this.milliseconds = milliseconds;
        }
        public void Debounce(Action action)
        {
            Cancel(lastCToken);
            var tokenSrc = lastCToken = new CancellationTokenSource();
            Task.Delay(milliseconds).ContinueWith(task =>
            {
                action();
            }, tokenSrc.Token);
        }
        public void Cancel(CancellationTokenSource source)
        {
            source?.Cancel();
        }
        public void Dispose()
        {
            Cancel(lastCToken);
        }
        ~Debouncer()
        {
            Dispose();
        }
    }
}
