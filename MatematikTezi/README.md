# EquaSolve — Denklem Çözücü ve Grafik Görselleştirme Projesi

**EquaSolve**, kullanıcıların matematiksel denklemleri/denklem sistemlerini çözebildiği ve grafiklerini görselleştirebildiği tam kapsamlı (full-stack) bir uygulamadır. Proje, bir **.NET Web API** backend'i ve bunu tüketen bir **Angular** frontend'inden oluşur; Docker ile birlikte çalışacak şekilde konteynerleştirilmiştir.

Bu proje, bir bitirme tezi kapsamında geliştirilmiştir.

## 📦 Alt Projeler

| Proje | Açıklama | README |
|---|---|---|
| **EquaSolve** (Backend) | Denklem çözme ve grafik analiz API'si — .NET Minimal API, MediatR, AutoMapper, Clean Architecture | [MathAPI/EquaSolve/README.md](./MathAPI/EquaSolve/README.md) |
| **EquaSolverAng** (Frontend) | Backend'i tüketen Angular arayüzü — Bootstrap, function-plot | [MathAngular/EquaSolverAng/README.md](./MathAngular/EquaSolverAng/README.md) |

## 🏗️ Genel Mimari

```
Kullanıcı ──▶ EquaSolverAng (Angular, Nginx) ──▶ EquaSolve API (.NET)
                     :4200                              :5000
```

- **Frontend**, kullanıcıdan denklem(ler) ve değişkenleri alır, backend'e istek atar.
- **Backend**, denklemi çözer / grafik uygunluğunu analiz eder ve sonucu (kökler, LaTeX, grafik bilgisi) JSON olarak döner.
- Her iki servis de kendi Dockerfile'ları ile imaj haline getirilip `docker-compose` üzerinden birlikte ayağa kaldırılır.

## 📁 Repo Yapısı

```
MatematikTezi/
├── MathAPI/
│   └── EquaSolve/              # Backend (.NET Web API)
│       ├── EquaSolve.Domain/
│       ├── EquaSolve.Application/
│       ├── EquaSolve.Infrastructure/
│       ├── EquaSolve.WebApp/
│       ├── Dockerfile
│       └── README.md
├── MathAngular/
│   └── EquaSolverAng/          # Frontend (Angular)
│       ├── src/
│       ├── Dockerfile
│       └── README.md
├── docker-compose.yml
└── README.md                   # Bu dosya
```

## 🚀 Kurulum ve Çalıştırma

Projeyi ayağa kaldırmak için tek gereksinim **Docker** ve **Docker Compose**'dur.

### 1. Depoyu klonlayın

Repo, birden fazla proje içeren bir monorepo olduğundan yalnızca bu proje klasörünü indirmek isterseniz `sparse-checkout` kullanabilirsiniz:

```bash
git clone --no-checkout https://github.com/Spacend00/MyFullStackProjects.git
cd MyFullStackProjects
git sparse-checkout init --cone
git sparse-checkout set MatematikTezi
git checkout main
cd MatematikTezi
```

Ya da tüm repoyu klonlayıp doğrudan `MatematikTezi` klasörüne geçebilirsiniz:

```bash
git clone https://github.com/Spacend00/MyFullStackProjects.git
cd MyFullStackProjects/MatematikTezi
```

### 2. Docker Compose ile ayağa kaldırın

```bash
docker compose up --build
```

Bu komut:
- **backend-api** servisini derleyip `equasolve_backend` konteyneri olarak `5000` portunda,
- **frontend-angular** servisini derleyip `equasolve_frontend` konteyneri olarak `4200` portunda

ayağa kaldırır. Frontend, backend servisinin (`depends_on`) ayağa kalkmasını bekler.

### 3. Uygulamaya erişin

| Servis | Adres |
|---|---|
| Angular arayüzü | http://localhost:4200 |
| API (Swagger/Scalar) | http://localhost:5000 |

### Servisleri durdurmak için

```bash
docker compose down
```

## 🐳 Docker Compose Yapılandırması

```yaml
version: '3.8'

services:
  backend-api:
    container_name: equasolve_backend
    build:
      context: ./MathAPI/EquaSolve
      dockerfile: Dockerfile
    ports:
      - "5000:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development

  frontend-angular:
    container_name: equasolve_frontend
    build:
      context: ./MathAngular/EquaSolverAng
      dockerfile: Dockerfile
    ports:
      - "4200:80"
    depends_on:
      - backend-api
```

- **Backend Dockerfile**: `mcr.microsoft.com/dotnet/sdk:9.0` imajı ile multi-stage olarak derlenir (proje dosyaları ayrı ayrı kopyalanıp `dotnet restore` çalıştırılır, ardından `dotnet publish` ile yayınlanır), son imaj olarak hafif `mcr.microsoft.com/dotnet/aspnet:9.0` runtime kullanılır.
- **Frontend Dockerfile**: `node:20` imajı ile `npm install` ve `npm run build -- --configuration=production` adımlarıyla production build alınır, ardından derlenen statik dosyalar `nginx:alpine` imajına kopyalanarak 80 portundan sunulur.

## 🛠️ Genel Teknoloji Özeti

| Katman | Teknolojiler |
|---|---|
| Backend | .NET Minimal API, MediatR, AutoMapper, Clean Architecture, OpenAPI + Scalar |
| Frontend | Angular, Bootstrap, function-plot |
| Altyapı | Docker, Docker Compose |

Detaylı bilgi için ilgili alt projenin README dosyasına bakabilirsiniz:
- [Backend README](./MathAPI/EquaSolve/README.md)
- [Frontend README](./MathAngular/EquaSolverAng/README.md)