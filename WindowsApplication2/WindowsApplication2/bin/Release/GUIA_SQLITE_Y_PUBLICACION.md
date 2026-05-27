# KUMO POS - Ejecucion portable con SQLite

## Que cambio

KUMO POS ahora usa una base local SQLite:

```text
Datos\KUMO.db
```

La aplicacion crea el archivo automaticamente en su primer inicio y agrega el catalogo de demostracion si la base esta vacia. Ya no utiliza `SQL Server Express`, nombres de servidor, instancias `SQLEXPRESS`, archivos `.bak` ni configuracion de red.

## Requisitos de la laptop destino

1. Windows 10 u 11.
2. .NET Framework 4.8 Runtime si Windows no lo tiene instalado.
3. Una carpeta donde el usuario pueda escribir archivos, por ejemplo Escritorio o Documentos.

No necesitas instalar SQLite: el proveedor y sus DLL nativas viajan dentro de la carpeta `Release`.

Enlace oficial para .NET Framework 4.8:

- <https://dotnet.microsoft.com/es-ES/download/dotnet-framework/net48>

## Publicar en Release

Desde Visual Studio:

1. Abre `WindowsApplication2.sln`.
2. Selecciona **Release** y **Any CPU**.
3. Ejecuta **Build > Rebuild Solution**.
4. Copia completa la carpeta `WindowsApplication2\WindowsApplication2\bin\Release\`.

Desde consola:

```powershell
MSBuild.exe .\WindowsApplication2\WindowsApplication2.sln /t:Rebuild /p:Configuration=Release /p:Platform="Any CPU"
```

## Archivos que debes copiar

No copies solamente el ejecutable. La carpeta de entrega debe conservar esta estructura:

```text
WindowsApplication2.exe
WindowsApplication2.exe.config
System.Data.SQLite.dll
Assets\
Datos\
    KUMOBD.sql
x86\
    SQLite.Interop.dll
x64\
    SQLite.Interop.dll
GUIA_SQLITE_Y_PUBLICACION.md
```

Al iniciar el sistema se creara:

```text
Datos\
    KUMO.db
```

Ese archivo contiene productos, inventario, ventas y pedidos. Para llevar los datos de una presentacion a otra computadora, cierra el sistema y copia tambien `Datos\KUMO.db`.

## Primera ejecucion

1. Copia la carpeta `Release` completa a una ubicacion escribible de la laptop.
2. Ejecuta `WindowsApplication2.exe`.
3. Inicia sesion con `admin` / `1234` o `usuario` / `1234`.
4. Entra a Caja; el primer inicio creara la base y cargara los productos iniciales.
5. Registra una venta de prueba y revisa Historial y Reportes.

## Solucion de problemas

| Problema | Accion |
| --- | --- |
| Falta `System.Data.SQLite.dll` o `SQLite.Interop.dll` | Copiar nuevamente toda la carpeta `Release`, incluyendo `x86` y `x64` |
| No se puede crear `KUMO.db` | Mover la carpeta del programa a Escritorio o Documentos, donde el usuario tenga permisos |
| Se desea comenzar la demo desde cero | Cerrar la app y renombrar o eliminar `Datos\KUMO.db`; se recreara al abrir |
| Se desea conservar ventas y productos | Copiar `Datos\KUMO.db` junto con el programa |

## Comprobacion antes de presentar

1. Abre el ejecutable desde la carpeta que vas a transportar.
2. Confirma que aparezcan los productos en Caja e Inventario.
3. Realiza una venta y valida Historial y Reportes.
4. Cierra y abre de nuevo el sistema para comprobar que `Datos\KUMO.db` conserva la informacion.
