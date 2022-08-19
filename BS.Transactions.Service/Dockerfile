#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BS.Transactions.Service/BS.Transactions.Service.csproj", "BS.Transactions.Service/"]
RUN dotnet restore "BS.Transactions.Service/BS.Transactions.Service.csproj"
COPY . .
WORKDIR "/src/BS.Transactions.Service"
RUN dotnet build "BS.Transactions.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BS.Transactions.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BS.Transactions.Service.dll"]