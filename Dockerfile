# Use the official ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProyectoColProfesionales/ProyectoColProfesionales.csproj", "ProyectoColProfesionales/"]
RUN dotnet restore "ProyectoColProfesionales/ProyectoColProfesionales.csproj"
COPY ProyectoColProfesionales/ ProyectoColProfesionales/
WORKDIR "/src/ProyectoColProfesionales"
RUN dotnet build "ProyectoColProfesionales.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ProyectoColProfesionales.csproj" -c Release -o /app/publish

# Use the base image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProyectoColProfesionales.dll"]