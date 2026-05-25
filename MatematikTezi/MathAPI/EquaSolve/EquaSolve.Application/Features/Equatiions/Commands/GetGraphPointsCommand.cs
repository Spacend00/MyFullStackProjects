
using EquaSolve.Domain.Entities;
using MediatR;

namespace EquaSolve.Application.Features.Equatiions.Commands
{
    public class GetGraphPointsCommand() : IRequest<GraphResult>
    {
        public string Equation { get; set; } = string.Empty;    
        public List<string> Variables { get; set; } = new List<string>();
    }
}
