
namespace EquaSolve.Infrastructure.Helpers
{
    public static class MathExpressionHelper
    {
        public static string Normalize(string equation)
        {
            string eq = equation;
            var cleanEq = eq.Replace(" ", "").ToLower();
            if (cleanEq.Contains("="))
            {
                var parts = cleanEq.Split('=');
                if (parts.Length > 2)
                {
                    throw new ArgumentException("Bir denklemde birden fazla eşittir (=) işareti olamaz!");
                }
                if (parts.Length < 2 || string.IsNullOrEmpty(parts[1]))
                {
                    throw new Exception("Eşittir işaretinin sağ tarafı boş bırakılamaz!");
                }

                string leftEq = parts[0];
                string rightEq = parts[1];

                if (rightEq == "0")
                {
                    eq = $"{leftEq}";
                }
                else if (leftEq == "0")
                {
                    eq = $"{rightEq}";
                }
                else
                {
                    eq = $"({leftEq}) - ({rightEq})";
                }
            }
            else
            {
                string cleanEqS = $"{cleanEq}";
                eq = cleanEqS;
            }

            return eq;

        }
    }
}
