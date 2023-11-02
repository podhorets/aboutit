FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AboutIt.Api/AboutIt.Api.csproj", "AboutIt.Api/"]
RUN dotnet restore "AboutIt.Api/AboutIt.Api.csproj"
COPY . .
WORKDIR "/src/AboutIt.Api"
RUN dotnet build "AboutIt.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AboutIt.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AboutIt.Api.dll"]
