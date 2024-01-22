# Use the official .NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app

# Build stage
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

# Copy the solution file and restore dependencies
COPY ["LiterallyAndUnironicallyBot.sln", "."]
COPY ["Bot.Base/Bot.Base.csproj", "Bot.Base/"]
COPY ["Bot.Data/Bot.Data.csproj", "Bot.Data/"]
COPY ["Bot.Services/Bot.Services.csproj", "Bot.Services/"]

RUN dotnet restore

# Copy the remaining source code
COPY . .

# Build the application
WORKDIR "/src/Bot.Base"
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bot.Base.dll"]
