FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

COPY . .   

# run tests on docker run
ENTRYPOINT ["dotnet", "test"]