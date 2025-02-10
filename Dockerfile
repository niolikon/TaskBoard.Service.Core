FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY ./output .
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_URLS=http://+:8080
ENTRYPOINT ["dotnet", "TaskBoard.Service.Core.Api.dll"]
