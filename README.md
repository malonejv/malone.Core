# malonejv.Core ![CI](https://github.com/malonejv/malone.Core/workflows/CI/badge.svg) [![Board Status](https://dev.azure.com/malonejv/45380abc-bef3-4509-b48e-888a31ec7319/d930ffdc-a2ca-481d-92f8-0a4471d4ee97/_apis/work/boardbadge/afbda97e-fb6a-4941-99e1-06a950f80cdb)](https://dev.azure.com/malonejv/45380abc-bef3-4509-b48e-888a31ec7319/_boards/board/t/d930ffdc-a2ca-481d-92f8-0a4471d4ee97/Microsoft.RequirementCategory/)

## Configuración

Para bajar dependencias de malone.Core que están alojadas en Github Package Registry hay que generar un Token. El propietario del repositorio debe ingresar a su al icono de su perfil, seleccionar ```settings / Developer settings / Personal Access Token```, generar un token.

Ese token debe registrarse con el CLI de nuget, ejecutando el siguiente comando:

```
nuget sources Add -Name
"github" -Source "https://nuget.pkg.github.com/malonejv/index.json" -UserName malonejv -Password TOKEN
```

**Importante:** Cerrar Visual Studio para que tome efecto el cambio.

Ese Token queda guardado en:

%userprofile%\AppData\Roaming\NuGet\NuGet.Config
