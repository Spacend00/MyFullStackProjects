# EquaSolve

> **Not:** Bu proje, [MyFullStackProjects](https://github.com/Spacend00/MyFullStackProjects) adlı monorepo içinde yer alan birden fazla projeden biridir. Bu README yalnızca `MatematikTezi/MathAPI/EquaSolve` klasöründeki API'yi kapsamaktadır.

**EquaSolve**, matematiksel denklemleri ve denklem sistemlerini çözen, ayrıca bir ifadenin grafik denklemi olup olmadığını analiz eden bir .NET Web API projesidir. Clean Architecture prensiplerine uygun olarak katmanlı bir yapıda geliştirilmiştir.

## ✨ Özellikler

- 🔢 Tek bir denklemi veya denklem sistemini çözme
- 📈 Verilen bir ifadenin grafik (implicit) denklem olup olmadığını tespit etme
- 🧮 Sembolik matematik motoru ile hem sayısal hem de kesir/kök içeren sonuçlar
- 📄 Sonuçların LaTeX formatında da döndürülmesi
- 🧱 Clean Architecture ile katmanlara ayrılmış, test edilebilir ve sürdürülebilir bir mimari

## 🏗️ Mimari

Proje, sorumlulukların net bir şekilde ayrıldığı 3 ana katmandan oluşur:

```
EquaSolve.Application/
├── DTOs/
│   └── EquationResponseDto.cs
├── Features/
│   └── Equations/
│       └── Commands/
│           ├── GetGraphPointsCommand.cs
│           ├── GetGraphPointsCommandHandler.cs
│           ├── SolveEquationCommand.cs
│           └── SolveEquationHandler.cs
├── Interfaces/
│   ├── IGraphSolverService.cs
│   └── IMathSolverService.cs
└── Mappings/
    └── MappingProfile.cs

EquaSolve.Domain/
└── Entities/
    ├── GraphResult.cs
    └── MathResult.cs

EquaSolve.Infrastructure/
├── Helpers/
│   └── MathExpressionHelper.cs
└── Services/
    ├── AngouriMathSolver.cs
    └── GraphSolverService.cs

EquaSolve.WebApp/
├── appsettings.json
└── Program.cs
```

- **Application**: İş kurallarını, CQRS komutlarını/handler'larını, DTO'ları ve arayüzleri içerir.
- **Domain**: Projenin temel varlıklarını (entity) barındırır, dış bağımlılığı yoktur.
- **Infrastructure**: Domain ve Application katmanlarındaki arayüzlerin somut implementasyonlarını (matematik çözücü, grafik servisi vb.) içerir.
- **WebApp**: Minimal API üzerinden dış dünyaya açılan giriş noktasıdır.

## 🛠️ Kullanılan Teknolojiler

| Teknoloji | Amaç |
|---|---|
| **.NET / Minimal API** | Hafif ve performanslı endpoint tanımlama |
| **MediatR** | CQRS pattern ile komut/handler ayrımı |
| **AutoMapper** | Domain nesneleri ile DTO'lar arası dönüşüm |
| **Dependency Injection** | Servislerin gevşek bağlı (loosely coupled) şekilde yönetilmesi |
| **OpenAPI + Scalar** | API dokümantasyonu ve interaktif test arayüzü |

## 📡 Endpointler

### `POST /api/solve`

Verilen denklemi veya denklem sistemini, belirtilen değişkenlere göre çözer.

**Tek bilinmeyenli denklem:**

Request:
```json
{
  "equations": ["(x^2)+3 =12"],
  "variables": ["x"]
}
```

Response:
```json
{
  "isSuccess": true,
  "message": "İşlem başarıyla tamamlandı!",
  "results": {
    "x": ["3", "-3"]
  },
  "latex": "\\begin{cases} 3 \\\\ -3 \\end{cases}"
}
```

**Doğrusal denklem sistemi (2 bilinmeyen):**

Request:
```json
{
  "equations": ["x + y =12", "x-y=3"],
  "variables": ["x", "y"]
}
```

Response:
```json
{
  "isSuccess": true,
  "message": "İşlem başarıyla tamamlandı!",
  "results": {
    "x": ["15/2"],
    "y": ["9/2"]
  },
  "latex": "\\begin{cases} \\begin{bmatrix}\\frac{15}{2} & \\frac{9}{2}\\end{bmatrix} \\end{cases}"
}
```

**Kök içeren (irrasyonel) sonuçlar üreten sistem:**

Request:
```json
{
  "equations": ["(x^2) + y =12", "x-y=3"],
  "variables": ["x", "y"]
}
```

Response:
```json
{
  "isSuccess": true,
  "message": "İşlem başarıyla tamamlandı!",
  "results": {
    "x": ["-1/2 * (1 + sqrt(61))", "(sqrt(61) - 1) / 2"],
    "y": ["-(-12 + (1 + sqrt(61)) ^ 2 / 4)", "-((sqrt(61) - 1) ^ 2 / 4 - 12)"]
  },
  "latex": "\\begin{cases} \\begin{bmatrix}\\frac{-1-\\sqrt{61}}{2} & ... \\end{bmatrix} \\end{cases}"
}
```

> Sonuçlar, sadeleştirilmiş sembolik ifadeler (kesir, kök vb.) olarak döner ve ek olarak LaTeX formatında da sunulur.

---

### `POST /api/graph`

Verilen ifadenin bir grafik (implicit) denklem olup olmadığını, kaç değişken içerdiğini ve geçerli olup olmadığını analiz eder.

**Örnek 1 — 2 değişken verildiğinde (implicit denklem):**

Request:
```json
{
  "equation": "(x^2)+3",
  "variables": ["x", "y"]
}
```

Response:
```json
{
  "isValid": true,
  "isImplicit": true,
  "errorMessage": null,
  "normalizedEquation": "(x^2)+3",
  "variableCount": 2
}
```

**Örnek 2 — tek değişken verildiğinde (implicit değil):**

Request:
```json
{
  "equation": "(x^2)+3",
  "variables": ["x"]
}
```

Response:
```json
{
  "isValid": true,
  "isImplicit": false,
  "errorMessage": null,
  "normalizedEquation": "(x^2)+3",
  "variableCount": 1
}
```

## 🐳 Çalıştırma

Bu proje, Angular istemcisiyle birlikte Docker üzerinden çalışacak şekilde yapılandırılmıştır. Kurulum ve çalıştırma adımları için ana repodaki [genel README](../README.md) dosyasına bakınız.