namespace AudioSignalAnalysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "440.wav";

            var parser = new AudioFileParser();
            var samples = parser.ParseAudioFile(filePath);

            Console.WriteLine("Length of the samples: " + samples.Length);
            var processor = new AudioProcessor();
            var frequencyDomainSamples = processor.ProcessAudioData(samples);

            var pronyMethod = new PronyMethod(frequencyDomainSamples, samples);
            pronyMethod.ImplementPronyMethod();

            // Visualization can be added here if needed
        }
    }
}