using MathNet.Symbolics;
namespace NewtonsMethod
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Welcome to newton's method");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("(note that the function must be in standart form like : 2x is written as 2*x)");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Insert function: f(x) =  ");
            var x = SymbolicExpression.Variable("x");
            string functionInput = Console.ReadLine();
            var function = SymbolicExpression.Parse(functionInput);
            double f(double xIn)
            {
                var inX = new Dictionary<string, FloatingPoint>
                    { { "x", xIn } };
                double result = function.Evaluate(inX).RealValue;
                return result;
            } // returns value of the function
            double[] functionVals = new double[1000];
            bool isFound = false;
            for(int i = 0; i < 1000; i++)
            {
                functionVals[i] = f(i);
            }
            for(int i = 0; i < functionVals.Length; i++)
            {
                if (functionVals[i] == 0) {
                    double root = functionVals[i];
                    Console.WriteLine("There was a root found : x = {0}", Array.IndexOf(functionVals, functionVals[i]) );
                    Console.ReadKey();
                    isFound = true;
                    break;
                }
                else if (functionVals[i] < 0)
                {
                    functionVals[i] *= -1;
                }
            }
            if (!isFound)
            {
                double minVal = functionVals.Min();
                int indexofVal = Array.IndexOf(functionVals, minVal);
                var derivitive = function.Differentiate("x");
                Console.Write("How precise root do you want to get? (a larger value means more precise) : ");
                int precision;
                if(!int.TryParse(Console.ReadLine(), out precision))
                {
                    Console.WriteLine("You have entered an invalid value - the precision is set to defult 10!");
                    precision = 10;
                }

                double fPrime(double xIn)
                {
                    var inX = new Dictionary<string, FloatingPoint>
                    { { "x", xIn } };
                    double result = derivitive.Evaluate(inX).RealValue;
                    return result;
                } // returns value of the derivitive
                double returnRoot = indexofVal;
                for (int i = 0; i< precision; i++)
                {
                    returnRoot = returnRoot - (f(returnRoot)) / (fPrime(returnRoot));
                }
                Console.WriteLine("The aproximation of the root is : x = {0}", returnRoot);
                Console.ReadKey();

            }
        }
    }
}
