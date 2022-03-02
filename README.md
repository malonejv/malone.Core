# malonejv.Core

| Branch      | Status                                                                                                                                                                             |
| ----------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Development | [![CI/CD](https://github.com/malonejv/malone.Core/actions/workflows/Workflow.yml/badge.svg)](https://github.com/malonejv/malone.Core/actions/workflows/Workflow.yml)               |
| Master      | [![CI/CD](https://github.com/malonejv/malone.Core/actions/workflows/Workflow.yml/badge.svg?branch=master)](https://github.com/malonejv/malone.Core/actions/workflows/Workflow.yml) |

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
