
namespace EquaSolve.Domain.Entities
{
    public class MathResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public Dictionary<string, List<string>> VariableValues { get; set; } = new();
        public string? LatexRepresentation { get; set; }
    }
}
