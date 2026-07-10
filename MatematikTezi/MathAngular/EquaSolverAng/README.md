# EquaSolverAng

> **Not:** Bu proje, [MyFullStackProjects](https://github.com/Spacend00/MyFullStackProjects) adlı monorepo içinde yer alan birden fazla projeden biridir. Bu README yalnızca Angular istemci uygulamasını kapsamaktadır. Backend için [EquaSolve API README](../EquaSolve/README.md) dosyasına bakabilirsiniz.

**EquaSolverAng**, [EquaSolve API](../EquaSolve)'yi tüketen, kullanıcının denklem/denklem sistemi girip anlık çözüm ve grafik görselleştirmesi alabildiği bir Angular arayüzüdür.

## ✨ Özellikler

- 🧮 Denklem veya denklem sistemi girerek anlık çözüm alma
- 📈 Girilen ifadenin grafiğini çizme (fonksiyon veya implicit denklem)
- 🎚️ Grafik için özelleştirilebilir eksen aralığı (X_min, X_max, Y_min, Y_max)
- 📚 Hazır örnek denklemler üzerinden hızlı deneme (Örnekler sayfası)
- 📄 Sonuçların hem kök hem de LaTeX formatında gösterimi

## 🏗️ Proje Yapısı

```
EquaSolverAng/
├── src/
│   └── app/
│       ├── main-page/          # Ana sayfa: denklem girişi, grafik ve sonuçlar
│       ├── examples-page/      # Hazır örnek denklemler sayfası
│       ├── graph-card/         # Grafik görselleştirme bileşeni
│       ├── result-card/        # Çözüm sonuçlarının gösterildiği bileşen
│       ├── models/
│       │   ├── equation.model.ts
│       │   └── graph-result.model.ts
│       ├── services/
│       │   ├── api.ts             # Solve endpoint'i ile iletişim
│       │   └── graph.service.ts   # Graph endpoint'i ile iletişim
│       ├── app.routes.ts
│       └── app.ts
├── Dockerfile
├── angular.json
└── package.json
```

## 🛠️ Kullanılan Teknolojiler

| Teknoloji | Amaç |
|---|---|
| **Angular** | SPA (Single Page Application) çatısı |
| **Bootstrap** | Sayfa düzeni ve UI bileşenleri |
| **function-plot** | Denklem/fonksiyon grafiklerinin çizilmesi (D3 tabanlı) |
| **Docker** | Konteynerleştirme ve dağıtım |

> **Not:** Proje içinde KaTeX kütüphanesi kuruludur, ancak API'den dönen LaTeX çıktısı şu an düz metin olarak gösterilmektedir; KaTeX ile render etme özelliği aktif değildir.

## 📄 Sayfalar

### Anasayfa (`main-page`)

- Kullanıcı, denklem(ler)i ve değişkenleri girer.
- Grafik için eksen aralığı (X_min/X_max, Y_min/Y_max) belirlenebilir.
- **Hesapla** butonuna basıldığında:
  - Girilen ifade `graph.service.ts` aracılığıyla `/api/graph` endpoint'ine gönderilerek grafik çizilebilir bir ifade olup olmadığı kontrol edilir.
  - Ardından `api.ts` aracılığıyla `/api/solve` endpoint'ine istek atılır ve kökler + LaTeX gösterimi `result-card` bileşeninde listelenir.
  - Sonuç grafiği `graph-card` bileşeni içinde **function-plot** ile çizilir.

### Örnekler (`examples-page`)

Kullanıcıların hızlıca deneyebileceği hazır denklem örnekleri üç kategoride sunulur:

- **Grafik Denklemleri**: Parabol, kübik fonksiyon, trigonometrik eğri, rasyonel fonksiyon, sönümlü sinüs dalgası, kalp eğrisi (implicit), hiperbolik fonksiyon, yüksek dereceden polinom.
- **Sonuçlar**: Seçilen örneğin kökleri ve LaTeX gösterimi.
- **Sistemler**: 3, 4 ve 5 bilinmeyenli lineer denklem sistemi örnekleri.

## 🐳 Çalıştırma

Bu proje, EquaSolve API ile birlikte Docker üzerinden çalışacak şekilde yapılandırılmıştır. Kurulum ve çalıştırma adımları için ana repodaki [genel README](../../README.md) dosyasına bakınız.