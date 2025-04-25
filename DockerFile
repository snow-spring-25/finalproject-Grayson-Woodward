FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN cd Final.Blazor
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
EXPOSE 8080
WORKDIR /App/Final.Blazor 
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Final.Blazor.dll"]