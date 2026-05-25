FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["EquaSolve.Domain/EquaSolve.Domain.csproj", "EquaSolve.Domain/"]
COPY ["EquaSolve.Application/EquaSolve.Application.csproj", "EquaSolve.Application/"]
COPY ["EquaSolve.Infrastructure/EquaSolve.Infrastructure.csproj", "EquaSolve.Infrastructure/"]
COPY ["EquaSolve.WebApp/EquaSolve.WebApp.csproj/", "EquaSolve.WebApp/"]

RUN dotnet restore "EquaSolve.WebApp/EquaSolve.WebApp.csproj"

COPY . .

WORKDIR "/src/EquaSolve.WebApp"
RUN dotnet publish "EquaSolve.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet", "EquaSolve.WebApp.dll" ]