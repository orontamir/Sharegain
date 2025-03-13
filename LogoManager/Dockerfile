FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /
COPY ["/", "LogoManager/"]
WORKDIR "/LogoManager"
RUN dotnet restore "LogoManager.csproj"
RUN dotnet build "LogoManager.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "LogoManager.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogoManager.dll"]
