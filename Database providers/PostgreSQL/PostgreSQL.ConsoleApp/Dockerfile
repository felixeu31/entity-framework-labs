FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["entity-framework-labs/Database providers/PostgreSQL/PostgreSQL.ConsoleApp/PostgreSQL.ConsoleApp.csproj", "PostgreSQL.ConsoleApp/"]
RUN dotnet restore "Database providers/PostgreSQL/PostgreSQL.ConsoleApp/PostgreSQL.ConsoleApp.csproj"
COPY . .
WORKDIR "/src/PostgreSQL.ConsoleApp"
RUN dotnet build "PostgreSQL.ConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PostgreSQL.ConsoleApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostgreSQL.ConsoleApp.dll"]
