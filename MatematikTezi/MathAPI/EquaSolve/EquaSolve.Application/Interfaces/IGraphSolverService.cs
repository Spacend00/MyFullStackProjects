
using EquaSolve.Domain.Entities;

namespace EquaSolve.Application.Interfaces
{
    public interface IGraphSolverService
    {
        Task<GraphResult> AnalyzeEquation(string equation, List<string> variables);
    }
}
