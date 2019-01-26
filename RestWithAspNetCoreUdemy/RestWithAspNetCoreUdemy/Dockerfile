FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["RestWithAspNetCoreUdemy/RestWithAspNetCoreUdemy.csproj", "RestWithAspNetCoreUdemy/"]
RUN dotnet restore "RestWithAspNetCoreUdemy/RestWithAspNetCoreUdemy.csproj"
COPY . .
WORKDIR "/src/RestWithAspNetCoreUdemy"
RUN dotnet build "RestWithAspNetCoreUdemy.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RestWithAspNetCoreUdemy.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RestWithAspNetCoreUdemy.dll"]