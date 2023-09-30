FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 80
EXPOSE 443

COPY Published/ /app/

ENTRYPOINT ["dotnet", "KutnoAPI.dll"]