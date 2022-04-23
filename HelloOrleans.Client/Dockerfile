#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HelloOrleans.Client/HelloOrleans.Client.csproj", "HelloOrleans.Client/"]
COPY ["HelloOrleans.Abstractions/HelloOrleans.Abstractions.csproj", "HelloOrleans.Abstractions/"]
RUN dotnet restore "HelloOrleans.Client/HelloOrleans.Client.csproj"
COPY . .
WORKDIR "/src/HelloOrleans.Client"
RUN dotnet build "HelloOrleans.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HelloOrleans.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloOrleans.Client.dll"]