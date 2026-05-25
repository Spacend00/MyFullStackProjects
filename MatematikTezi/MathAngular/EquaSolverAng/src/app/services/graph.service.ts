// services/graph.service.ts
import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GraphResult } from '../models/graph-result.model';

@Injectable({ providedIn: 'root' })
export class GraphService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7217/api/graph';

  analyze(equation: string, variables: string[]) {
    return this.http.post<GraphResult>(this.apiUrl, { equation, variables });
  }
}