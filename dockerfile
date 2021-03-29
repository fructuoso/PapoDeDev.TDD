# Stage 1
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /build
COPY /src .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Stage 2
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS final
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "PapoDeDev.TDD.WebAPI.dll"]