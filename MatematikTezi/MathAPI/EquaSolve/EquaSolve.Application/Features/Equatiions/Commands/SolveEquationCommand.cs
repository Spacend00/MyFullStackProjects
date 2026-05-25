
using EquaSolve.Application.DTOs;
using MediatR;

namespace EquaSolve.Application.Features.Equatiions.Commands
{
    public record SolveEquationCommand(
        List<string> Equations,
        List<string> Variables) : IRequest<EquationResponseDto>;
}
