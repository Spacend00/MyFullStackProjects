export interface EquationRequest{
    equations: string[];
    variables: string[];
}

export interface Point{
    v1: number;
    v2: number;
}

export interface EquationResponse{
    isSuccess: boolean;
    message: string;
    results: {[key: string]: string};
    latex: string;
}