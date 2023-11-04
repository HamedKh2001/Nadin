FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Nadin.WebAPI/Nadin.WebAPI.csproj", "src/Nadin.WebAPI/"]
COPY ["src/BuildingBlocks/SharedKernel/SharedKernel.csproj", "src/BuildingBlocks/SharedKernel/"]
COPY ["src/Nadin.Application/Nadin.Application.csproj", "src/Nadin.Application/"]
COPY ["src/Nadin.Domain/Nadin.Domain.csproj", "src/Nadin.Domain/"]
COPY ["src/Nadin.Infrastucture/Nadin.Infrastucture.csproj", "src/Nadin.Infrastucture/"]
RUN dotnet restore "src/Nadin.WebAPI/Nadin.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Nadin.WebAPI"
RUN dotnet build "Nadin.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nadin.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nadin.WebAPI.dll"]