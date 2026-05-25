
namespace EquaSolve.Domain.Entities
{
    public class GraphResult
    {
        public bool IsValid { get; set; }
        public bool IsImplicit { get; set; }
        public string? ErrorMessage { get; set; }
        public string NormalizedEquation { get; set; } = string.Empty;
        public int VariableCount { get; set; }
    }
}
