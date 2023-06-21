FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RondesSuisse_Web/RondesSuisse_Web.csproj", "RondesSuisse_Web/"]
RUN dotnet restore "RondesSuisse_Web/RondesSuisse_Web.csproj"
COPY . .
WORKDIR "/src/RondesSuisse_Web"
RUN dotnet build "RondesSuisse_Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RondesSuisse_Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RondesSuisse_Web.dll"]
