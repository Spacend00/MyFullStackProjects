import { Component, inject, signal, input } from '@angular/core';
import { GraphCard } from '../graph-card/graph-card';
import { ResultCard } from '../result-card/result-card';
import { GraphService } from '../services/graph.service';
import { Api } from '../services/api';
import { GraphResult } from '../models/graph-result.model';

@Component({
  selector: 'app-examples-page',
  imports: [GraphCard, ResultCard],
  templateUrl: './examples-page.html',
  styleUrl: './examples-page.css',
})
export class ExamplesPage {
  private api = inject(Api);
  private graphService = inject(GraphService);

  solveResult = signal<any>(null); 

  plots = signal<GraphResult[]>([]);

  activeView = signal<'graph' | 'result'>('graph');

  isSolving = signal<boolean>(false);

  grafikOrnekleri = [
    { label: 'Parabol Grafiği', eq: 'x^2 = y' },
    { label: 'Kübik Fonksiyon', eq: 'x^3 - 3x = y' },
    { label: 'Trigonometrik Eğri', eq: 'sin(x) = y' },
    { label: 'Rasyonel Fonksiyon', eq: '1 / (x^2 - 1) = y' },
    { label: 'Sönümlü Sinüs Dalgası', eq: 'sin(x) * exp(-0.2*x) = y' },
    { label: 'Kalp Eğrisi (Kapalı Fonksiyon)', eq: '(x^2 + y^2 - 1)^3 - x^2 * y^3 = 0' },
    { label: 'Hiperbolik Fonksiyon', eq: 'cosh(x) = y' },
    { label: 'Yüksek Dereceden Polinom', eq: 'x^5 - 5x^3 + 4x = y' }
  ];

  sistemOrnekleri = [
    { 
    label: '3 Bilinmeyenli Lineer Sistem', 
    eq: ['2*x + y - z = 8','-3*x - y + 2*z = -11','-2*x + y + 2*z = -3'], 
    vars: 'x, y, z' 
    },
    { 
    label: '4 Bilinmeyenli Köprü Matrisi', 
    eq: ['a + b + c + d = 10','a - b + 2*c - d = 3','2*a + b - c + 2*d = 13','-a + 2*b + c - d = 1'], 
    vars: 'a, b, c, d' 
    },
    { 
    label: '5 Bilinmeyenli Sistem',
    eq: ['1*v + 1*w + 1*x + 1*y + 1*z = 15','1*v - 1*w + 2*x + 1*y - 1*z = 5','2*v + 1*w - 1*x + 1*y + 1*z = 11','1*v + 2*w + 1*x - 1*y + 1*z = 9','1*v + 1*w + 1*x + 2*y - 1*z = 9'],
    vars: 'v, w, x, y, z' 
    }
  ];

  grafikSec(denklem: string) {
    // Grafik seçildiğinde eski ortak çözüm sonuçlarını sıfırlıyoruz
    this.activeView.set('graph');

    this.graphService.analyze(denklem, ['x', 'y']).subscribe(res => {
      if (res.isValid) {
        this.plots.set([res]); // Sadece tek bir grafik çizdiriyoruz
      }
    });
  }

  sistemSec(ornek: { eq: string[], vars: string }) {
    this.isSolving.set(true);
    this.activeView.set('result');

    const equations = ornek.eq
    const variables = ornek.vars.split(',').map(v => v.trim());

    // A) Önce grafik çizim listesini dolduralım (Canlı çizim mantığı gibi)
    const tempPlots: GraphResult[] = [];
    equations.forEach((eq, index) => {
      this.graphService.analyze(eq, variables).subscribe(res => {
        if (res.isValid) {
          tempPlots[index] = res;
          this.plots.set([...tempPlots]); // Sinyali güncelle, grafik kartı çizsin
        }
      });
    });

    // B) Sonra API'den ortak çözümü ve LaTeX'i isteyelim
    const payload = { equations, variables };
    this.api.solveEquation(payload).subscribe({
      next: (res) => {
        this.solveResult.set(res);
      },
      error: (err) => {
        console.error(err);
      },
      complete: () => {
        this.isSolving.set(false);
      }
    });
  }
}
