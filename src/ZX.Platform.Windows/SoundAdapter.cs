using NAudio.Wave;

using ZX.Core.Cpu;
using ZX.Core.Spectrum;

namespace ZX.Platform.Windows;

public class SoundAdapter
{
    readonly SpectrumRuntime runtime;

    readonly List<long> list = [];
    bool initialState;
    bool lastState;

    const int SampleRate = 44100;
    const int SamplesPerFrame = SampleRate / CpuRuntime.FramesPerSecond;
    const int TStatesPerSample = (int)(CpuRuntime.TStatesPerFrame / (double)SamplesPerFrame);

    readonly byte[] buffer = new byte[SamplesPerFrame * 2];

    readonly BufferedWaveProvider waveProvider;
    readonly WaveOutEvent waveOut;

    public SoundAdapter(SpectrumRuntime runtime)
    {
        this.runtime = runtime;

        waveProvider = new(new(SampleRate, 16, 1))
        {
            DiscardOnBufferOverflow = true,
            BufferLength = SampleRate * 2,
        };

        waveOut = new WaveOutEvent();
        waveOut.Init(waveProvider);
        waveOut.Play();
    }

    public void RunFrame()
        => ProduceSound();

    public void HandleOutput(byte address, byte value)
    {
        //TODO: put into correct bucket

        if (address == 0xFE)
        {
            var state = (value & 0b10000) == 0b10000;

            if (state != lastState)
            {
                list.Add(runtime.CurrentTick);
                //buckets[runtime.CurrentTick / TStatesPerSample];
                lastState = state;
            }
        }
    }

    void ProduceSound()
    {
        if (list.Count > 0)
        {
            var amp = (ushort)(ushort.MaxValue / 2);
            var currentState = initialState;
            var currentToggleIndex = 0;

            for (var i = 0; i < SamplesPerFrame; ++i)
            {
                var intervalStart = i * TStatesPerSample;
                var intervalEnd = intervalStart + TStatesPerSample - 1;

                long currentIntervalTrue = 0;
                long currentStateStart = intervalStart;

                while (currentToggleIndex < list.Count)
                {
                    var toggle = list[currentToggleIndex];
                    if (toggle > intervalEnd)
                        break;

                    var segmentLength = toggle - currentStateStart;

                    if (currentState)
                        currentIntervalTrue += segmentLength;

                    currentState = !currentState;
                    currentStateStart = toggle;
                    currentToggleIndex++;
                }

                var remainingLength = intervalEnd - currentStateStart + 1;
                if (currentState)
                    currentIntervalTrue += remainingLength;

                var ratio = (double)currentIntervalTrue / TStatesPerSample;

                var sample = (short)(2.0 * (ratio - 0.5) * amp);
                //var sample = (short)(ratio >= 0.5 ? amp : -amp);

                buffer[i * 2] = (byte)(sample & 0xFF);
                buffer[i * 2 + 1] = (byte)(sample >> 8);
            }

            waveProvider.AddSamples(buffer, 0, buffer.Length);

            list.Clear(); //TODO: optimize

            initialState = lastState;
        }
    }

    public void Stop()
    {
        waveOut.Stop();
        waveOut.Dispose();
    }
}
