FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["AtonWebApi/AtonWebApi.csproj", "AtonWebApi/"]
RUN dotnet restore "AtonWebApi/AtonWebApi.csproj"
COPY . .
WORKDIR "/src/AtonWebApi"
RUN dotnet build "AtonWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AtonWebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AtonWebApi.dll"]