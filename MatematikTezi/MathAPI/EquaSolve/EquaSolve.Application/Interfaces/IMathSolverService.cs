
using EquaSolve.Domain.Entities;

namespace EquaSolve.Application.Interfaces
{
    public interface IMathSolverService
    {
        Task<MathResult> SolveAsync(List<string> equations, List<string> variables);
    }
}
