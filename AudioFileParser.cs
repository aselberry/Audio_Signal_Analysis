using System;
using System.Linq;
using System.Numerics;
using NAudio.Wave;

namespace AudioSignalAnalysis
{
    public class AudioFileParser
    {
        public Complex[] ParseAudioFile(string filePath)
        {
            using (var reader = new AudioFileReader(filePath))
            {
                int bufferSize = (int)reader.Length;
                var buffer = new float[bufferSize];
                int read = reader.Read(buffer, 0, bufferSize);
                Complex[] samples = new Complex[read];
                for (int i = 0; i < read; i++)
                {
                    samples[i] = new Complex(buffer[i], 0);
                }

                return samples;
            }
        }
    }
}