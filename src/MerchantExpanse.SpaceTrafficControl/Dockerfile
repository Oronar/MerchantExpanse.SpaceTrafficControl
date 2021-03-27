FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/MerchantExpanse.SpaceTrafficControl/MerchantExpanse.SpaceTrafficControl.csproj", "src/MerchantExpanse.SpaceTrafficControl/"]
COPY ["src/MerchantExpanse.SpaceTrafficControl.ViewModels/MerchantExpanse.SpaceTrafficControl.ViewModels.csproj", "src/MerchantExpanse.SpaceTrafficControl.ViewModels/"]
RUN dotnet restore "src/MerchantExpanse.SpaceTrafficControl/MerchantExpanse.SpaceTrafficControl.csproj"
COPY . .
WORKDIR "/src/src/MerchantExpanse.SpaceTrafficControl"
RUN dotnet build "MerchantExpanse.SpaceTrafficControl.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MerchantExpanse.SpaceTrafficControl.csproj" -c Release -o /app/publish


FROM nginx:alpine AS final
EXPOSE 80

WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY src/MerchantExpanse.SpaceTrafficControl/nginx.conf /etc/nginx/nginx.conf