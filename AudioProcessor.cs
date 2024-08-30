using System;
using System.Numerics;
using MathNet.Numerics.IntegralTransforms;

namespace AudioSignalAnalysis
{
    public class AudioProcessor
    {
        public Complex[] ProcessAudioData(Complex[] samples)
        {
            Complex[] complexSamples = new Complex[samples.Length];
            for (int i = 0; i < samples.Length; i++)
            {
                complexSamples[i] = samples[i];
            }

            Fourier.Forward(complexSamples, FourierOptions.Default);
            return complexSamples;
        }
    }
}