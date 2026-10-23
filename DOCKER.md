# API.UISIL en Docker y Render

Ejecuta los comandos desde la carpeta que contiene `API.UISIL.slnx` y el `Dockerfile`.

## Probar localmente

Instala e inicia Docker Desktop con contenedores Linux. Luego:

```powershell
docker build -t api-uisil .
$env:ConnectionStrings__Conexion = 'Server=tcp:SERVIDOR,1433;Database=ProgramacionAvanzada;User ID=USUARIO;Password=CLAVE;Encrypt=True;TrustServerCertificate=False'
docker run --rm -p 8080:8080 -e ConnectionStrings__Conexion api-uisil
```

La API queda en `http://localhost:8080`. Usa una ruta de un controlador para probarla; la ruta `/` puede responder 404. OpenAPI solo se habilita en Development en el Program.cs actual.

La variable `ConnectionStrings__Conexion` reemplaza `ConnectionStrings:Conexion` de appsettings.json, sin cambiar el código ni guardar credenciales en Git.

## SQL Server

La configuración actual usa `localhost` y `Integrated Security=True`. Dentro del contenedor, localhost apunta al propio contenedor, y la autenticación de Windows de tu equipo no se hereda.

Para conectarte al SQL Server de tu equipo desde Docker Desktop, usa `host.docker.internal` como servidor, habilita TCP/IP y usa un usuario SQL con acceso a la base. El puerto debe coincidir con el de tu instancia. Para desarrollo con un certificado autofirmado puedes necesitar `TrustServerCertificate=True`.

En Render necesitas un SQL Server accesible desde el servicio, con la base, tablas y procedimientos almacenados ya creados. Usa su dirección real y credenciales SQL; `host.docker.internal` no apunta a tu computadora desde Render. Este Dockerfile incluye la API; la base de datos se administra por separado.

## Publicar en Render

1. Sube los archivos nuevos a tu repositorio Git.
2. Crea un **Web Service** y conecta el repositorio.
3. Selecciona **Docker** como lenguaje.
4. Usa como contexto de construcción la raíz que contiene `API.UISIL.slnx` y las cuatro carpetas de proyectos. Si esa es la raíz del repositorio, deja Root Directory vacío y Dockerfile Path en `./Dockerfile`.
5. Agrega la variable secreta `ConnectionStrings__Conexion` con la conexión al SQL Server remoto. No incluyas `Integrated Security=True`.
6. Deja Docker Command vacío y despliega.

El contenedor escucha en `0.0.0.0` y usa la variable PORT de Render, con 8080 como valor local predeterminado. No necesitas configurar Build Command ni Start Command: el Dockerfile compila e inicia la API.

Documentación: https://render.com/docs/docker y https://render.com/docs/web-services#port-binding
