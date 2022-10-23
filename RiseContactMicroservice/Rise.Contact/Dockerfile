#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Rise.Contact/Rise.Contact.csproj", "Rise.Contact/"]
COPY ["Rise.MongoDb.Entity/Rise.MongoDb.Entity.csproj", "Rise.MongoDb.Entity/"]
COPY ["Rise.Shared/Rise.Shared.csproj", "Rise.Shared/"]
RUN dotnet restore "Rise.Contact/Rise.Contact.csproj"
COPY . .
WORKDIR "/src/Rise.Contact"
RUN dotnet build "Rise.Contact.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rise.Contact.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rise.Contact.dll"]