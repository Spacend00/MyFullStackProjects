import { Component, signal, effect, ElementRef, viewChild, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Api } from '../services/api';
import { GraphService } from '../services/graph.service';
import { ResultCard } from '../result-card/result-card';
import { GraphCard } from '../graph-card/graph-card';
import { GraphResult } from '../models/graph-result.model';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [FormsModule, ResultCard, GraphCard],
  templateUrl: './main-page.html',
  styleUrl: './main-page.css',
})
export class MainPage {
  private api = inject(Api);
  private graphService = inject(GraphService);

  public Object: any = Object;

  equationText = signal<string>(''); 
  variableText = signal<string>('x, y');
  
  solveResult = signal<any>(null); 
  
  plots = signal<GraphResult[]>([]);
  xMin = signal<number>(-5);
  xMax = signal<number>(5);
  yMin = signal<number>(-5);
  yMax = signal<number>(5);

  constructor() {
    effect(() => {
      const equations = this.equationText().split('\n').filter(e => e.trim() !== '');
      const variables = this.variableText().split(',').map(v => v.trim());
      
      if (equations.length === 0) {
        this.plots.set([]);
        return;
      }

      equations.forEach((eq, index) => {
        this.graphService.analyze(eq, variables).subscribe(res => {
          if (res.isValid) {
            const current = [...this.plots()];
            current[index] = res;
            this.plots.set(current);
          }
        });
      });
    });    
  }

  solve() {
    const data = {
      equations: this.equationText().split('\n').filter(e => e.trim() !== ''),
      variables: this.variableText().split(',').map(v => v.trim())
    };

    this.api.solveEquation(data).subscribe(res => {
      this.solveResult.set(res); 
    });
  }

  sinirDegisti(eksen: 'xMin' | 'xMax' | 'yMin' | 'yMax', deger: string) {
    const sayi = parseFloat(deger);
    if (!isNaN(sayi)) {

      const sinyalHaritasi = {
        xMin: this.xMin,
        xMax: this.xMax,
        yMin: this.yMin,
        yMax: this.yMax
      }

      sinyalHaritasi[eksen].set(sayi);
    }
  }
}