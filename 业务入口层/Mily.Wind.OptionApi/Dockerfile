#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["业务入口层/Mily.Wind.OptionApi/Mily.Wind.OptionApi.csproj", "业务入口层/Mily.Wind.OptionApi/"]
COPY ["业务接口层/Mily.Wind.OptionApplication/Mily.Wind.OptionApplication.csproj", "业务接口层/Mily.Wind.OptionApplication/"]
COPY ["业务逻辑层/Mily.Wind.OptionLogic/Mily.Wind.OptionLogic.csproj", "业务逻辑层/Mily.Wind.OptionLogic/"]
COPY ["公用基础层/Mily.Wind.Extens/Mily.Wind.Extens.csproj", "公用基础层/Mily.Wind.Extens/"]
COPY ["组件服务层/Mily.Wind.LogPlugin/Mily.Wind.LogPlugin.csproj", "组件服务层/Mily.Wind.LogPlugin/"]
COPY ["公用基础层/Mily.Wind.VMod/Mily.Wind.VMod.csproj", "公用基础层/Mily.Wind.VMod/"]
COPY ["组件服务层/Mily.Wind.QuartzPlugin/Mily.Wind.QuartzPlugin.csproj", "组件服务层/Mily.Wind.QuartzPlugin/"]
COPY ["组件服务层/Mily.Wind.Plugin/Mily.Wind.Plugin.csproj", "组件服务层/Mily.Wind.Plugin/"]
RUN dotnet restore "业务入口层/Mily.Wind.OptionApi/Mily.Wind.OptionApi.csproj"
COPY . .
WORKDIR "/src/业务入口层/Mily.Wind.OptionApi"
RUN dotnet build "Mily.Wind.OptionApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mily.Wind.OptionApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mily.Wind.OptionApi.dll"]