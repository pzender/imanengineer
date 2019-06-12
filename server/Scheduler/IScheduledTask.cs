using System.Threading;
using System.Threading.Tasks;

namespace TV_App.Scheduler
{
    public interface IScheduledTask
    {
        string Schedule { get; }
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}