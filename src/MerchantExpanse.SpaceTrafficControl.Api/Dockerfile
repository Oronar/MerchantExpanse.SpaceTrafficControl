#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/MerchantExpanse.SpaceTrafficControl.Api/MerchantExpanse.SpaceTrafficControl.Api.csproj", "src/MerchantExpanse.SpaceTrafficControl.Api/"]
COPY ["src/MerchantExpanse.SpaceTrafficControl.ViewModels/MerchantExpanse.SpaceTrafficControl.ViewModels.csproj", "src/MerchantExpanse.SpaceTrafficControl.ViewModels/"]
RUN dotnet restore "src/MerchantExpanse.SpaceTrafficControl.Api/MerchantExpanse.SpaceTrafficControl.Api.csproj"
COPY . .
WORKDIR "/src/src/MerchantExpanse.SpaceTrafficControl.Api"
RUN dotnet build "MerchantExpanse.SpaceTrafficControl.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MerchantExpanse.SpaceTrafficControl.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MerchantExpanse.SpaceTrafficControl.Api.dll"]