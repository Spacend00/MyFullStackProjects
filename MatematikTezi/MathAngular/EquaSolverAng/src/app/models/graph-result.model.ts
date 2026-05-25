export interface GraphResult {
  normalizedEquation: string;
  isValid: boolean;
  isImplicit: boolean;
  variableCount: number;
  errorMessage?: string;
}