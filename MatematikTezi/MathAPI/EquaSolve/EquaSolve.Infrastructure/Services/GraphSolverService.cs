
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
                // 1. Normalizasyon: "y = x^2" -> "x^2 - y" veya "x^2 + y^2 = 1" -> "x^2 + y^2 - 1"
                string normalized = MathExpressionHelper.Normalize(equation);

                // 2. Geçerlilik Kontrolü: AngouriMath parse edemezse hata fırlatır
                Entity expr = normalized;

                // 3. Tip Belirleme: Kaç tane değişken kullanılmış?
                // Eğer hem x hem y varsa bu bir Implicit (kapalı) denklemdir.
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
