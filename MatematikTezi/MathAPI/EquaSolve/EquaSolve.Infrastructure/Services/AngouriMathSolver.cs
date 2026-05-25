
using AngouriMath;
using EquaSolve.Application.Interfaces;
using EquaSolve.Domain.Entities;
using EquaSolve.Infrastructure.Helpers;

namespace EquaSolve.Infrastructure.Services
{
    public class AngouriMathSolver : IMathSolverService
    {
        public async Task<MathResult> SolveAsync(List<string> equations, List<string> variables)
        {
            var result = new MathResult();

            try
            {
                if (variables.Count == equations.Count)
                {
                    List<string> formattedList = NormalizeEquations(equations);
                    var equationEntities = formattedList.Select(e => MathS.FromString(e)).ToArray();
                    var variableEntities = variables.Select(v => MathS.Var(v)).ToArray();

                    var solutions = MathS.Equations(equationEntities.ToArray()).Solve(variableEntities);

                    if (solutions != null)
                    {
                        var latexLines = solutions.Select(e => e.Latexise()).ToList();
                        result.LatexRepresentation = @"\begin{cases} " + string.Join(@" \\ ", latexLines) + @" \end{cases}";
                        foreach (var solution in solutions)
                        {
                            ParseSolution(solution, variables, result);
                        }
                        result.IsSuccess = true;
                        result.Message = "İşlem başarıyla tamamlandı!";
                    }
                }
                else
                {
                    result.Message = "İşlem başarıyla tamamlandı! (Grafik modu: Tekil çözüm aranmadı)";
                }
                result.IsSuccess = true;
                if (string.IsNullOrEmpty(result.Message)) result.Message = "İşlem başarıyla tamamlandı!";

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Sistem çözülürken hata oluştu: {ex.Message}";
            }

            return result;
        }

        public List<string> NormalizeEquations(List<string> equations)
        {
            return equations.Select(e => MathExpressionHelper.Normalize(e)).ToList();
        }

        private void ParseSolution(Entity solution, List<string> variables, MathResult result)
        {
            if (solution == null) return;

            try
            {
                Entity formatSolution = FormatSmartResult(solution);
                if (formatSolution is Entity.Matrix)
                {
                    var matrix = (Entity.Matrix)formatSolution;
                    for (int i = 0; i < variables.Count; i++)
                    {
                        string varName = variables[i];
                        string val = matrix[0, i].ToString();
                        if (!result.VariableValues.ContainsKey(varName))
                        {
                            result.VariableValues[varName] = new List<string>();
                        }

                        result.VariableValues[varName].Add(val);
                    }
                }
                else
                {
                    string val = formatSolution.ToString();

                    for (int i = 0; i < variables.Count; i++)
                    {
                        string varName = variables[i];

                        // Liste yoksa oluştur
                        if (!result.VariableValues.ContainsKey(varName))
                        {
                            result.VariableValues[varName] = new List<string>();
                        }

                        // Değeri ekle
                        result.VariableValues[varName].Add(val);
                    }
                }

            }
            catch (Exception ex)
            {
                result.Message += $" [Formatlama Hatası: {ex.Message}]";
            }

            if (result.VariableValues.Count == 0)
            {
                result.Message += $" [Sistem Çözdü Ama Parse Edemedi. Tip: {solution.GetType().Name}, Ham: {solution}]";
            }
        }

        

        private Entity FormatSmartResult(Entity element)
        {
            Entity simplified = element.Simplify();

            if (simplified is Entity.Number.Integer)
            {
                return simplified;
            }
            else if (simplified is Entity.Number.Rational rational)
            {
                return rational;
            }
            else if (simplified.ToString().Contains("sqrt") || simplified.ToString().Contains("^"))
            {
                return simplified;
            }
            else if (simplified.ToString().Contains("i"))
            {
                return simplified;
            }

            try
            {
                var numeric = simplified.EvalNumerical();

                if (numeric is Entity.Number.Real)
                {
                    return Math.Round((double)numeric, 2).ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            catch
            {

            }

            return simplified;
        }
    }
}
