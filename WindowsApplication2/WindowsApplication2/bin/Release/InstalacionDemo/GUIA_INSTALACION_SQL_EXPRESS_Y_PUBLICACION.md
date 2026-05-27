# KUMO POS - Instalacion en laptop de presentacion

## Diagnostico del problema corregido

La aplicacion estaba configurada con `Server=UZZIEL\SQLEXPRESS;Database=KUMOBD;Integrated security=true`. Ese valor solo funciona en la computadora llamada `UZZIEL`. En una segunda laptop, el programa intentaba localizar esa maquina y por eso no cargaba el catalogo.

La conexion quedo configurada para una instancia local estandar de SQL Server Express:

```text
Server=.\SQLEXPRESS;Database=KUMOBD;Integrated Security=True;Connect Timeout=5;Application Name=KUMO POS
```

`.\SQLEXPRESS` significa: "la instancia SQLEXPRESS instalada en esta misma laptop". No se localizaron referencias a `DESKTOP-XXXX`, `LocalDB`, archivos `.mdf` adjuntos ni rutas locales de base de datos dentro del proyecto.

## Que instalar en la laptop destino

1. **Microsoft SQL Server 2025 Express**. Es la edicion gratuita vigente publicada por Microsoft para aplicaciones de escritorio y debe instalarse con la instancia nombrada `SQLEXPRESS`.
2. **.NET Framework 4.8 Runtime** si la laptop no lo tiene. El proyecto fue construido para .NET Framework 4.5.2 y la version 4.8 ejecuta aplicaciones de esa familia.
3. **SQL Server Management Studio 22 (SSMS)** solo si se va a restaurar un respaldo `.bak` o inspeccionar la base visualmente. Para la instalacion automatica incluida no es obligatorio.

Enlaces oficiales:

- SQL Server Express: <https://www.microsoft.com/en-us/sql-server/sql-server-downloads>
- SSMS 22: <https://learn.microsoft.com/en-us/ssms/install/install>
- .NET Framework 4.8 Runtime: <https://dotnet.microsoft.com/es-ES/download/dotnet-framework/net48>

## Ruta recomendada para una demo limpia

### 1. Instalar SQL Server Express

1. Descarga **SQL Server 2025 Express** desde el enlace oficial.
2. Ejecuta el instalador como administrador.
3. Elige la instalacion de Express y conserva o selecciona el nombre de instancia `SQLEXPRESS`.
4. Usa autenticacion de Windows y agrega al usuario de Windows que ejecutara KUMO POS como administrador de SQL Server cuando el instalador lo permita.
5. Al terminar, abre **Servicios** o **SQL Server Configuration Manager** y comprueba que exista y este iniciado `SQL Server (SQLEXPRESS)`.

La aplicacion y SQL Server se ejecutan en la misma laptop, por lo que no se necesita habilitar conexiones remotas ni iniciar SQL Server Browser para la presentacion.

### 2. Copiar la aplicacion

Copia completa la carpeta generada en:

```text
WindowsApplication2\WindowsApplication2\bin\Release\
```

No copies solo el `.exe`, porque la conexion y el instalador de la base viajan en archivos adicionales.

### 3. Crear y preparar KUMOBD automaticamente

Dentro de la carpeta copiada, abre PowerShell en `InstalacionDemo` y ejecuta:

```powershell
Set-ExecutionPolicy -Scope Process Bypass
.\ConfigurarBaseDatosDemo.ps1
```

El script se conecta a `.\SQLEXPRESS`, crea `KUMOBD` si no existe, agrega las columnas actuales del sistema si la base estaba incompleta y deja productos iniciales para poder presentar la caja. Puede ejecutarse otra vez sin borrar ventas ni duplicar el catalogo.

El resultado esperado incluye:

```text
KUMOBD esta lista para KUMO POS.
Servidor: .\SQLEXPRESS
Productos disponibles: 6
```

### 4. Ejecutar el sistema

1. Regresa a la carpeta principal del programa.
2. Ejecuta `WindowsApplication2.exe`.
3. Inicia sesion con `admin` / `1234`. Tambien se conserva el alias `usuario` / `1234` para la demo.
4. Entra a Caja e Inventario y comprueba que se muestren los productos.
5. Registra una venta de prueba y valida que aparezca en Historial y Reportes.

## Restaurar los datos reales con un archivo .bak

Usa esta opcion cuando quieras llevar las ventas o productos exactos de tu laptop actual, en lugar del catalogo inicial.

### En la laptop original

1. Instala o abre SSMS y conectate a `.\SQLEXPRESS`.
2. Haz clic derecho en la base `KUMOBD`, elige **Tasks > Back Up**.
3. Genera el archivo `KUMOBD_Demo.bak` y copialo junto con la carpeta `Release`.

### En la laptop de presentacion

1. Instala SQL Server Express y SSMS.
2. Abre SSMS y conectate al servidor `.\SQLEXPRESS` con autenticacion de Windows.
3. Haz clic derecho en **Databases**, elige **Restore Database**, selecciona `KUMOBD_Demo.bak` y restaura la base con nombre `KUMOBD`.
4. Si ya ejecutaste la instalacion automatica y SSMS indica que `KUMOBD` existe, marca la opcion para sobrescribir la base solo si ya no necesitas esos datos de prueba.
5. Despues de restaurar, ejecuta `InstalacionDemo\ConfigurarBaseDatosDemo.ps1`. Esto completa columnas nuevas sin eliminar los datos restaurados.

Importante: un respaldo creado en una version mas nueva de SQL Server no puede restaurarse en una version mas antigua. Para evitarlo, instala en el destino SQL Server Express de la misma version o una version posterior a la de la laptop original.

## Configurar la cadena de conexion

El archivo que usa el ejecutable publicado es:

```text
WindowsApplication2.exe.config
```

Debe contener esta cadena dentro del ajuste `Con_Croche`:

```xml
<value>Server=.\SQLEXPRESS;Database=KUMOBD;Integrated Security=True;Connect Timeout=5;Application Name=KUMO POS</value>
```

Solo cambia `Server` si instalaste expresamente otra instancia. Ejemplos:

```text
Server=.\SQLEXPRESS        Instancia local recomendada
Server=LAPTOP01\SQLEXPRESS Instancia en una computadora especifica
```

Para la demo se recomienda conservar `.\SQLEXPRESS`, porque no depende del nombre de la laptop.

## Mensajes de conexion que ahora muestra el sistema

El programa valida la base al abrir la caja y ya distingue los casos comunes:

| Mensaje o situacion | Solucion |
| --- | --- |
| No existe o no inicia `SQLEXPRESS` | Instalar SQL Server Express o iniciar `SQL Server (SQLEXPRESS)` |
| Existe SQL Server pero falta `KUMOBD` | Ejecutar `InstalacionDemo\ConfigurarBaseDatosDemo.ps1` |
| Faltan tablas o columnas | Ejecutar nuevamente el script de instalacion |
| Inicio de sesion rechazado | Ejecutar la app con el usuario autorizado o dar permiso en SQL Server |

## Publicar en modo Release

### Desde Visual Studio

1. Abre `WindowsApplication2.sln`.
2. Selecciona la configuracion **Release** y plataforma **Any CPU**.
3. Ejecuta **Build > Rebuild Solution**.
4. Usa el contenido completo de `WindowsApplication2\WindowsApplication2\bin\Release\`.

### Desde consola de desarrollador

```powershell
MSBuild.exe .\WindowsApplication2\WindowsApplication2.sln /p:Configuration=Release /p:Platform="Any CPU"
```

## Archivos que debes llevar a la presentacion

Copia la carpeta `bin\Release` completa. Debe contener, como minimo:

```text
WindowsApplication2.exe
WindowsApplication2.exe.config
Assets\
InstalacionDemo\
    ConfigurarBaseDatosDemo.ps1
    KUMOBD.sql
    GUIA_INSTALACION_SQL_EXPRESS_Y_PUBLICACION.md
```

Si deseas presentar la informacion real, agrega:

```text
KUMOBD_Demo.bak
```

No es necesario copiar `.vs`, `obj`, el codigo fuente ni los archivos `.pdb` para ejecutar la demostracion.

## Comprobacion rapida antes de presentar

1. Revisa que este iniciado `SQL Server (SQLEXPRESS)`.
2. Ejecuta una vez `ConfigurarBaseDatosDemo.ps1`.
3. Abre KUMO POS y verifica productos en Caja.
4. Captura una venta de prueba.
5. Confirma la venta en Historial y Reportes.
6. Cierra y vuelve a abrir el programa para confirmar que conserva los datos.
