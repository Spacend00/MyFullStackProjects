
using AutoMapper;
using EquaSolve.Application.Interfaces;
using EquaSolve.Domain.Entities;
using MediatR;

namespace EquaSolve.Application.Features.Equatiions.Commands
{
    public class GetGraphPointsCommandHandler : IRequestHandler<GetGraphPointsCommand, GraphResult>
    {
        private readonly IGraphSolverService _graphSolverService;
        public GetGraphPointsCommandHandler(IGraphSolverService graphSolverService, IMapper mapper) 
        {
            _graphSolverService = graphSolverService;
        }
        async Task<GraphResult> IRequestHandler<GetGraphPointsCommand, GraphResult>.Handle(GetGraphPointsCommand request, CancellationToken cancellationToken)
        {
            return await _graphSolverService.AnalyzeEquation(request.Equation, request.Variables);
        }
    }
}
