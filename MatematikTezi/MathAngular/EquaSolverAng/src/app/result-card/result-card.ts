import { KeyValuePipe } from '@angular/common';
import { Component, effect, ElementRef, signal, Input,input, viewChild } from '@angular/core';

@Component({
  selector: 'app-result-card',
  imports: [KeyValuePipe],
  templateUrl: './result-card.html',
  styleUrl: './result-card.css',
})
export class ResultCard {
  solveResult = signal<any>(null);
  katexBox = viewChild<ElementRef>('katexBox');
  isLoading = input<boolean>(false);

  @Input() set solveData(value: any) {
    this.solveResult.set(value);
  }

  get formattedPoints(){
    const results = this.solveResult()?.results;
    if (!results || Object.keys(results).length === 0) return [];

    const varNames = Object.keys(results);
    const rowCount = results[varNames[0]].length;

    const points = [];
    for (let i = 0; i < rowCount; i++){
      const point: any = {};
      varNames.forEach(v => {
        point[v] = results[v][i]
      });
      points.push(point);
    }
    return points;
  }

  constructor() {
    effect(() => {
      const result = this.solveResult();
      const box = this.katexBox();

      if(result && result.latex && box){
        box.nativeElement.innerHTML = result.latex;
      }
    });
  }
}
