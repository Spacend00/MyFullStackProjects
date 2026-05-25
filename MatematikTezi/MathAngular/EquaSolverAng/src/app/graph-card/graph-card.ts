import { Component, ElementRef, signal, viewChild, effect, input } from '@angular/core';
import { GraphResult } from '../models/graph-result.model';
import functionPlot from 'function-plot';

@Component({
  selector: 'app-graph-card',
  imports: [],
  templateUrl: './graph-card.html',
  styleUrl: './graph-card.css',
})
export class GraphCard {
  plots = input<GraphResult[]>([]);
  xMin = input<number>(-5);
  xMax = input<number>(5);
  yMin = input<number>(-5);
  yMax = input<number>(5);

  graphContainer = viewChild<ElementRef>('graphContainer');

  constructor() {
    effect(() => {
      const dataToDraw = this.plots();
      const container = this.graphContainer();

      const limits: { xMin: number, xMax: number, yMin: number, yMax: number } = {
        xMin: this.xMin(),
        xMax: this.xMax(),
        yMin: this.yMin(),
        yMax: this.yMax()
      };

      if (container) {
        this.renderAll(dataToDraw, container.nativeElement, limits);
      }
    });
  }

  private renderAll(results: GraphResult[], element: HTMLElement, limits: { xMin: number, xMax: number, yMin: number, yMax: number}) {
    element.innerHTML = '';
    
    // function-plot birden fazla denklemi aynı anda alabilir!
    const series = results.map(r => ({
      fn: r.normalizedEquation,
      fnType: r.isImplicit ? 'implicit' : 'linear' as any
    }));

    functionPlot({
      target: element,
      width: element.clientWidth || 450,
      height: 390,
      grid: true,
      xAxis: {
        label: 'x Ekseni',
        domain: [limits.xMin, limits.xMax]
      },
      yAxis: {
        label: 'y Ekseni',
        domain: [limits.yMin, limits.yMax]
      },
      tip: {
        xLine: true,
        yLine: true,
        renderer: (x, y, index) => {
          return `Kordinat: (${x.toFixed(2)}, ${y.toFixed(2)})`;
        }
      },
      data: series // Diziyi doğrudan veriyoruz
    });
  }
}
