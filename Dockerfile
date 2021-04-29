#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 8081

RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BiliFor.Api/BiliFor.Api.csproj", "BiliFor.Api/"]
COPY ["BiliFor.Extensions/BiliFor.Extensions.csproj", "BiliFor.Extensions/"]
COPY ["BiliFor.EventBus/BiliFor.EventBus.csproj", "BiliFor.EventBus/"]
COPY ["BiliFor.Common/BiliFor.Common.csproj", "BiliFor.Common/"]
COPY ["BiliFor.Tasks/BiliFor.Tasks.csproj", "BiliFor.Tasks/"]
COPY ["BiliFor.IServices/BiliFor.IServices.csproj", "BiliFor.IServices/"]
COPY ["BiliFor.Model/BiliFor.Model.csproj", "BiliFor.Model/"]
COPY ["BiliFor.Services/BiliFor.Services.csproj", "BiliFor.Services/"]
COPY ["BiliFor.Repository/BiliFor.Repository.csproj", "BiliFor.Repository/"]
RUN dotnet restore "BiliFor.Api/BiliFor.Api.csproj"
COPY . .
WORKDIR "/src/BiliFor.Api"
RUN dotnet build "BiliFor.Api.csproj" -c Release -o /app/build



FROM build AS publish
RUN dotnet publish "BiliFor.Api.csproj" -c Release -o /app/publish
COPY BiliFor.Api/BiliFor.xml  /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BiliFor.Api.dll"]