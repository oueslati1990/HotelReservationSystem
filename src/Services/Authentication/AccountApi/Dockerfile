#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Authentication/AccountApi/AccountApi.csproj", "Services/Authentication/AccountApi/"]
RUN dotnet restore "Services/Authentication/AccountApi/AccountApi.csproj"
COPY . .
WORKDIR "/src/Services/Authentication/AccountApi"
RUN dotnet build "AccountApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccountApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountApi.dll"]
