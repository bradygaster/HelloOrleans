#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HelloOrleans.Dashboard/HelloOrleans.Dashboard.csproj", "HelloOrleans.Dashboard/"]
COPY ["HelloOrleans.Abstractions/HelloOrleans.Abstractions.csproj", "HelloOrleans.Abstractions/"]
COPY ["HelloOrleans.Grains/HelloOrleans.Grains.csproj", "HelloOrleans.Grains/"]
RUN dotnet restore "HelloOrleans.Dashboard/HelloOrleans.Dashboard.csproj"
COPY . .
WORKDIR "/src/HelloOrleans.Dashboard"
RUN dotnet build "HelloOrleans.Dashboard.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HelloOrleans.Dashboard.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloOrleans.Dashboard.dll"]