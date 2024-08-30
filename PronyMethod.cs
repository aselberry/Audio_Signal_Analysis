using System;
using System.Linq;
using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace AudioSignalAnalysis
{
    public class PronyMethod
    {
        public Complex[] functionValues;
        public Complex[] input;

        public PronyMethod(Complex[] functionValues, Complex[] input)
        {
            this.functionValues = functionValues;
            this.input = input;
        }

        public MathNet.Numerics.LinearAlgebra.Vector<double> ConstructPolLER(int M)
        {
            var fVector = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray(functionValues.Take(M).Select(c => c.Magnitude).ToArray());
            var A = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.Dense(M, M);
            Console.WriteLine($"The half of the function values: {M}");
            var y = -fVector.SubVector(M, functionValues.Length - M);

            for (int i = 0; i < M; i++)
            {
                A.SetRow(i, fVector.SubVector(i, M).ToArray());
            }

            var p = A.Solve(y);
            return p;
        }

        public MathNet.Numerics.LinearAlgebra.Vector<double> TestingConstructFValues(Complex[] x, MathNet.Numerics.LinearAlgebra.Vector<double> lambdas, MathNet.Numerics.LinearAlgebra.Vector<double> coeffs)
        {
            int n = lambdas.Count;
            var f = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(x.Length);
            for (int k = 0; k < n; k++)
            {
                f += coeffs[k] * MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(x.Select(xi => Math.Exp(lambdas[k] * xi.Magnitude)).ToArray());
            }

            return f;
        }

        public MathNet.Numerics.LinearAlgebra.Vector<double> FindCoefficientsCJ(MathNet.Numerics.LinearAlgebra.Vector<double> alphas, MathNet.Numerics.LinearAlgebra.Vector<double> f)
        {
            int numberOfAlphas = alphas.Count;
            var A = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.Dense(numberOfAlphas, numberOfAlphas);
            for (int i = 0; i < numberOfAlphas; i++)
            {
                A.SetRow(i, MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(numberOfAlphas, j => Math.Pow(Math.Exp(alphas[i]), j)).ToArray());
            }

            var coefficients = A.Solve(f);
            return coefficients;
        }

        public void ImplementPronyMethod()
        {
            int m = functionValues.Length / 2 - 1;
            var P = ConstructPolLER(m);
            var PConcat = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfEnumerable(new[] { 1.0 }.Concat(P.Reverse()));
            var polynomial = new Polynomial(PConcat.ToArray());
            var zeros = polynomial.Roots(); 
            var estimatedLambda = zeros.Select(z => Complex.Log(z)).Select(c => c.Real).ToArray();
            var estimatedCoefficient = FindCoefficientsCJ(MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray(estimatedLambda), MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray(functionValues.Take(m).Select(c => c.Magnitude).ToArray()));
            var fEst = TestingConstructFValues(input, MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray(estimatedLambda), estimatedCoefficient);

            // Output or use the results as needed
            Console.WriteLine("Prony Method Analysis Complete");
        }
    }
}