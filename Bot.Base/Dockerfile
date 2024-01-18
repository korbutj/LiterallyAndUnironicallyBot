FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bot.Base/Bot.Base.csproj", "Bot.Base/"]
COPY ["Bot.Data/Bot.Data.csproj", "Bot.Data/"]
COPY ["Bot.Services/Bot.Services.csproj", "Bot.Services/"]
RUN dotnet restore "Bot.Base/Bot.Base.csproj"
COPY . .
WORKDIR "/src/Bot.Base"
RUN dotnet build "Bot.Base.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Bot.Base.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bot.Base.dll"]
