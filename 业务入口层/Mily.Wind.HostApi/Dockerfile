#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Mily.Wind.HostApi/Mily.Wind.HostApi.csproj", "Mily.Wind.HostApi/"]
COPY ["Mily.Wind.Logic/Mily.Wind.Logic.csproj", "Mily.Wind.Logic/"]
COPY ["Mily.Wind.SugarContext/Mily.Wind.SugarContext.csproj", "Mily.Wind.SugarContext/"]
COPY ["Mily.Wind.SugarEntity/Mily.Wind.SugarEntity.csproj", "Mily.Wind.SugarEntity/"]
COPY ["Mily.Wind.Extens/Mily.Wind.Extens.csproj", "Mily.Wind.Extens/"]
COPY ["Mily.Wind.VMod/Mily.Wind.VMod.csproj", "Mily.Wind.VMod/"]
RUN dotnet restore "Mily.Wind.HostApi/Mily.Wind.HostApi.csproj"
COPY . .
WORKDIR "/src/Mily.Wind.HostApi"
RUN dotnet build "Mily.Wind.HostApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mily.Wind.HostApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mily.Wind.HostApi.dll"]