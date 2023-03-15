FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GroupBy.Web.API/GroupBy.Web.API.csproj", "GroupBy.Web.API/"]
COPY ["GroupBy.Application/GroupBy.Application.csproj", "GroupBy.Application/"]
COPY ["GroupBy.Core/GroupBy.Domain.csproj", "GroupBy.Core/"]
COPY ["GroupBy.Infrastructure/GroupBy.Data.csproj", "GroupBy.Infrastructure/"]
RUN dotnet restore "GroupBy.Web.API/GroupBy.Web.API.csproj"
COPY . .
WORKDIR "/src/GroupBy.Web.API"
RUN dotnet build "GroupBy.Web.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GroupBy.Web.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GroupBy.Web.API.dll"]