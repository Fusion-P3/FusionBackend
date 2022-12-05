FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
WORKDIR /ECommerce.API
RUN dotnet publish -c release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /
COPY --from=build-env /ECommerce.API/out .
ENTRYPOINT ["dotnet", "ECommerce.API.dll"]