FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["TestProject.WebAPI.csproj", "TestProject.WebAPI/"]
RUN dotnet restore "TestProject.WebAPI.csproj"
COPY . .
WORKDIR "/src/TestProject.WebAPI"
RUN dotnet build "TestProject.WebAPI.csproj" -c Development -o /app/build

FROM build AS publish
RUN dotnet publish "TestProject.WebAPI.csproj" -c Development -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestProject.WebAPI.dll"]