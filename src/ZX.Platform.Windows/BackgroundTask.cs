using System.Collections.Concurrent;

namespace ZX.Platform.Windows;

public class BackgroundTask(Func<CancellationToken, Task> func, CancellationToken cancel)
{
    private readonly ConcurrentQueue<Action> delegateQueue = new();

    private Task backgroundTask = default!;

    public Task RunAsync()
    {
        backgroundTask = Task.Run(() => Process(cancel), cancel);
        return backgroundTask;
    }

    private async Task Process(CancellationToken cancel)
    {
        do
        {
            await func(cancel);

            if (delegateQueue.TryDequeue(out var action))
                action.Invoke();
        }
        while (!cancel.IsCancellationRequested);
    }

    public void Dispatch(Action action)
        => delegateQueue.Enqueue(action);

    public void Wait()
        => backgroundTask.Wait();
}
