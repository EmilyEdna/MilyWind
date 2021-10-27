#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["业务入口层/Mily.Wind.LogApi/Mily.Wind.LogApi.csproj", "业务入口层/Mily.Wind.LogApi/"]
COPY ["业务接口层/Mily.Wind.LogApplication/Mily.Wind.LogApplication.csproj", "业务接口层/Mily.Wind.LogApplication/"]
COPY ["业务逻辑层/Mily.Wind.LogLogic/Mily.Wind.LogLogic.csproj", "业务逻辑层/Mily.Wind.LogLogic/"]
COPY ["公用基础层/Mily.Wind.Extens/Mily.Wind.Extens.csproj", "公用基础层/Mily.Wind.Extens/"]
COPY ["组件服务层/Mily.Wind.LogPlugin/Mily.Wind.LogPlugin.csproj", "组件服务层/Mily.Wind.LogPlugin/"]
COPY ["公用基础层/Mily.Wind.VMod/Mily.Wind.VMod.csproj", "公用基础层/Mily.Wind.VMod/"]
COPY ["组件服务层/Mily.Wind.QuartzPlugin/Mily.Wind.QuartzPlugin.csproj", "组件服务层/Mily.Wind.QuartzPlugin/"]
COPY ["组件服务层/Mily.Wind.Plugin/Mily.Wind.Plugin.csproj", "组件服务层/Mily.Wind.Plugin/"]
RUN dotnet restore "业务入口层/Mily.Wind.LogApi/Mily.Wind.LogApi.csproj"
COPY . .
WORKDIR "/src/业务入口层/Mily.Wind.LogApi"
RUN dotnet build "Mily.Wind.LogApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mily.Wind.LogApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mily.Wind.LogApi.dll"]