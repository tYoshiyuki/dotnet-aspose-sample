FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
RUN apt-get update && \
  apt-get install -y wget && \
  apt-get install -y zip unzip && \
  apt-get install -y fontconfig
RUN wget --secure-protocol=TLSv1 --no-check-certificate https://moji.or.jp/wp-content/ipafont/IPAexfont/IPAexfont00301.zip
RUN unzip IPAexfont00301.zip
RUN mkdir -p /usr/share/fonts/ipa
RUN cp IPAexfont00301/*.ttf /usr/share/fonts/ipa
RUN fc-cache -fv

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DotNetAsposeSample/DotNetAsposeSample.csproj", "DotNetAsposeSample/"]
COPY ["DotnetAsposeSample.Lib/DotnetAsposeSample.Lib.csproj", "DotnetAsposeSample.Lib/"]
RUN dotnet restore "DotNetAsposeSample/DotNetAsposeSample.csproj"
COPY . .
WORKDIR "/src/DotNetAsposeSample"
RUN dotnet build "DotNetAsposeSample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetAsposeSample.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetAsposeSample.dll"]
