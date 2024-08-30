using System;
using OxyPlot;
using OxyPlot.Series;

public class Visualizer
{
    public PlotModel CreateFrequencyPlot(double[] magnitudes, int sampleRate)
    {
        var plotModel = new PlotModel { Title = "Frequency Plot" };
        var lineSeries = new LineSeries();

        int numBins = magnitudes.Length;
        for (int i = 0; i < numBins; i++)
        {
            double frequency = (double)i * sampleRate / numBins;
            lineSeries.Points.Add(new DataPoint(frequency, magnitudes[i]));
        }

        plotModel.Series.Add(lineSeries);
        Console.WriteLine("Success"); // Indicate success when the method finishes
        return plotModel;
    }
}