
using AutoMapper;
using EquaSolve.Application.DTOs;
using EquaSolve.Application.Interfaces;
using MediatR;

namespace EquaSolve.Application.Features.Equatiions.Commands
{
    public class SolveEquationHandler : IRequestHandler<SolveEquationCommand, EquationResponseDto>
    {
        private readonly IMathSolverService _mathSolver;
        private readonly IMapper _mapper;

        public SolveEquationHandler(IMathSolverService solverService, IMapper mapper)
        {
            _mathSolver = solverService;
            _mapper = mapper;
        }
        public async Task<EquationResponseDto> Handle(SolveEquationCommand request, CancellationToken cancellationToken)
        {
            var domainResult = await _mathSolver.SolveAsync(request.Equations, request.Variables);
            return _mapper.Map<EquationResponseDto>(domainResult);
        }
    }
}
