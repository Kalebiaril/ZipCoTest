FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
#RUN mkdir TestProject.WebAPI
COPY ["./TestProject.WebAPI.csproj", "./TestProject.WebAPI/"]
RUN dotnet restore "./TestProject.WebAPI/TestProject.WebAPI.csproj"
COPY . ./TestProject.WebAPI/
WORKDIR "/src/TestProject.WebAPI"
RUN dotnet build "TestProject.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestProject.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestProject.WebAPI.dll"]




