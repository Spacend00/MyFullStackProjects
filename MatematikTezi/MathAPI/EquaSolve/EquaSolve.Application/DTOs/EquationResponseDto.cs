
namespace EquaSolve.Application.DTOs
{
    public class EquationResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public Dictionary<string, List<string>> Results { get; set; } = new();
        public string? Latex { get; set; }
    }
}
