# CryoStatus

[![Steam](https://img.shields.io/badge/Steam-Descargar-blue?logo=steam)](https://steamcommunity.com/sharedfiles/filedetails/?id=3186019180)
[![Version](https://img.shields.io/badge/Version-1.0.0-blue.svg)](https://steamcommunity.com/sharedfiles/filedetails/?id=3186019180)
[![Language](https://img.shields.io/badge/Language-C%23-blue.svg)](https://steamcommunity.com/sharedfiles/filedetails/?id=3186019180)

Este es un script para Space Engineers que muestra el estado de las cámaras criogénicas en paneles LCD.

## Uso

1. Suscríbete al mod en Steam.
2. Coloca el script `CryoStatus.cs` en un bloque programable en tu nave o base.
3. Renombra las cámaras criogénicas y los paneles LCD correspondientes con los tags apropiados.

## Configuración

El script utiliza tags para identificar las cámaras criogénicas y los paneles LCD. Puedes cambiar estos tags en el script si lo deseas.

```csharp
// Tags

// Nombre de las cámaras criogénicas
string cryoChamberName = "CryoChamber";
// Nombre de los paneles LCD
string lcdPanelName = "CryoStatusPanel";
```

Para el uso de multiples camaras criogenicas, se debe cambiar el Nombre de las camaras criogenicas y los paneles LCD.

### Ejemplo

Si se tiene 3 camaras criogenicas y 3 paneles LCD, se debe cambiar los nombres de la siguiente manera:

#### Camara Criogenica 0

- Nombre: `CryoChamber 0`
- Panel LCD: `CryoStatusPanel 0`

#### Camara Criogenica 1

- Nombre: `CryoChamber 1`
- Panel LCD: `CryoStatusPanel 1`

#### Camara Criogenica 2

- Nombre: `CryoChamber 2`
- Panel LCD: `CryoStatusPanel 2`
  


## Contribuir

Si tienes alguna sugerencia o encuentras algún error, por favor, abre un problema en este repositorio.

## Licencia

Este script está disponible bajo la licencia Apache License 2.0. Consulta el archivo `LICENSE` para más detalles.