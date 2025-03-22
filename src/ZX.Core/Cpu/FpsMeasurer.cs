using System.Diagnostics;

namespace ZX.Core.Cpu;

public class FpsMeasurer
{
    readonly long frequency = Stopwatch.Frequency;

    long intervalStart = Stopwatch.GetTimestamp();
    int count;
    int fps;

    public int Update()
    {
        count++;
        long now = Stopwatch.GetTimestamp();

        if (now - intervalStart >= frequency)
        {
            fps = count;
            count = 0;
            intervalStart += frequency;
        }

        return fps;
    }
}
