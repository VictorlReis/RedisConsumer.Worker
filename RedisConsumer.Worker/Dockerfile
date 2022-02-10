#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RedisConsumer.Worker/RedisConsumer.Worker.csproj", "RedisConsumer.Worker/"]
RUN dotnet restore "RedisConsumer.Worker/RedisConsumer.Worker.csproj"
COPY . .
WORKDIR "/src/RedisConsumer.Worker"
RUN dotnet build "RedisConsumer.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedisConsumer.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedisConsumer.Worker.dll"]