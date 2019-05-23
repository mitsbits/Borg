FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Clients/Borg.Web.Client/Borg.Web.Client.csproj", "src/Clients/Borg.Web.Client/"]
COPY ["src/System/Borg.System.Licencing/Borg.System.Licencing.csproj", "src/System/Borg.System.Licencing/"]
COPY ["src/Framework/Borg.Framework/Borg.Framework.csproj", "src/Framework/Borg.Framework/"]
COPY ["src/Infrastructure/Borg.Infrastructure.Core/Borg.Infrastructure.Core.csproj", "src/Infrastructure/Borg.Infrastructure.Core/"]
COPY ["src/System/Borg.System.Backoffice/Borg.System.Backoffice.csproj", "src/System/Borg.System.Backoffice/"]
COPY ["src/System/Borg.System.Backoffice.Lib/Borg.System.Backoffice.Lib.csproj", "src/System/Borg.System.Backoffice.Lib/"]
COPY ["src/System/Borg.System.Backoffice.Security/Borg.System.Backoffice.Security.csproj", "src/System/Borg.System.Backoffice.Security/"]
COPY ["src/Framework/Borg.Framework.MVC/Borg.Framework.MVC.csproj", "src/Framework/Borg.Framework.MVC/"]
COPY ["src/Platform/Borg.Platform.Backoffice.Security.EF/Borg.Platform.Backoffice.Security.EF.csproj", "src/Platform/Borg.Platform.Backoffice.Security.EF/"]
COPY ["src/Framework/Borg.Framework.EF/Borg.Framework.EF.csproj", "src/Framework/Borg.Framework.EF/"]
COPY ["src/Framework/Borg.Framework.Reflection/Borg.Framework.Reflection.csproj", "src/Framework/Borg.Framework.Reflection/"]
COPY ["src/Platform/Borg.Platform.Dispatch.NetCore/Borg.Platform.Dispatch.NetCore.csproj", "src/Platform/Borg.Platform.Dispatch.NetCore/"]
COPY ["src/Framework/Borg.Framework.Dispatch/Borg.Framework.Dispatch.csproj", "src/Framework/Borg.Framework.Dispatch/"]
RUN dotnet restore "src/Clients/Borg.Web.Client/Borg.Web.Client.csproj"
COPY . .
WORKDIR "/src/src/Clients/Borg.Web.Client"
RUN dotnet build "Borg.Web.Client.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Borg.Web.Client.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Borg.Web.Client.dll"]