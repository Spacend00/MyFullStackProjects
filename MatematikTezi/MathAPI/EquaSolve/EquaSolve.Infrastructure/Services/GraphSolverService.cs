
using AngouriMath;
using EquaSolve.Application.Interfaces;
using EquaSolve.Domain.Entities;
using EquaSolve.Infrastructure.Helpers;

namespace EquaSolve.Infrastructure.Services
{
    public class GraphSolverService : IGraphSolverService
    {
        public async Task<GraphResult> AnalyzeEquation(string equation, List<string> variables)
        {
            try
            {
                string normalized = MathExpressionHelper.Normalize(equation);

                Entity expr = normalized;

                bool isImplicit = variables.Count > 1;

                return new GraphResult
                {
                    NormalizedEquation = normalized,
                    IsValid = true,
                    IsImplicit = isImplicit,
                    VariableCount = variables.Count,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new GraphResult
                {
                    IsValid = false,
                    ErrorMessage = "Geçersiz matematiksel ifade: " + ex.Message
                };
            }
        }
    }
}
