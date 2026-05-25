import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EquationRequest, EquationResponse } from '../models/equation.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Api {
  private url = "http://localhost:5000/api/solve";

  constructor(private http: HttpClient){}

  solveEquation(data: EquationRequest): Observable<EquationResponse> {
    return this.http.post<EquationResponse>(this.url, data)
  }
}
