**Hola, Marc!**

Per provar la solució que he desenvolupat, segueix aquests passos que t'ajudaran a configurar i executar els dos projectes (.NET Core) que formen part d'aquest sistema ERP.

### 1. Requisits previs

Per poder executar els projectes, necessitaràs:

- **Visual Studio 2022** o una versió superior.
- **SQL Server** per poder crear la base de dades i provar les funcionalitats.
- **.NET SDK** compatible amb .NET Core 8, que és la versió que he utilitzat per desenvolupar la solució.

### 2. Configurar la base de dades

1. **Script SQL de creació de la base de dades**: 
   - Dins del projecte `ERP.Api`, trobaràs un fitxer SQL anomenat `CreateDatabase.sql`, situat a la carpeta `Scripts`.
   - Aquest script crea la base de dades `ERP` amb una taula `Client`, i ja afegeix tres registres de prova perquè puguis veure dades reals.

2. **Executar l'script**: 
   - Has d'executar aquest script SQL a la teva instància de SQL Server, ja sigui a través del SQL Server Management Studio (SSMS) o qualsevol altra eina SQL.
   - **Nota**: Si us plau, assegura't que tens permisos suficients per crear bases de dades i que tens les credencials d'accés correctes.

### 3. Configuració de la cadena de connexió i la URL de l'API

Un cop tinguis la base de dades creada, necessitaràs modificar tant la cadena de connexió com la URL de l'API:

- **Cadena de connexió (per a `ERP.Api`)**:
  - Obre el fitxer `appsettings.json` dins del projecte `ERP.Api`.
  - Actualitza la cadena de connexió amb les dades de la teva instància SQL:

    ```json
    {
      "ConnectionStrings": {
        "ERPDatabase": "Server=EL_TEU_SERVIDOR;Database=ERP;User Id=EL_TEU_USUARI;Password=LA_TEVA_CONTRASENYA;"
      }
    }
    ```

- **URL de l'API (per a `ERP.Web`)**:
  - Al fitxer `appsettings.json` dins del projecte `ERP.Web`, trobaràs una secció `ApiSettings` on podràs configurar la URL base de l'API.
  - Actualitza aquesta URL amb la ruta correcta de `ERP.Api` (per exemple, `https://localhost:5001`):

    ```json
    {
      "ApiSettings": {
        "BaseUrl": "https://localhost:5001/api/client"
      }
    }
    ```

### 4. Executar els projectes

1. **Obrir la solució a Visual Studio**:
   - Obre el fitxer `.sln` de la solució amb Visual Studio.

2. **Configurar projectes d'inici múltiples**:
   - Fes clic dret a la solució i selecciona **Set Startup Projects**.
   - Configura els dos projectes (`ERP.Api` i `ERP.Web`) perquè s'iniciïn alhora.

3. **Iniciar la solució**:
   - Executa la solució. Això obrirà dues finestres del navegador:
     - `ERP.Api`, que serveix l'API en una URL (per exemple, `https://localhost:5001`).
     - `ERP.Web`, que serveix la interfície web que consumeix l'API (per exemple, `https://localhost:5002`).

### 5. Provar l'aplicació web

1. **Accedir a la interfície web**:
   - Accedeix a la URL de `ERP.Web` (per exemple, `https://localhost:5002`) i veuràs una llista de clients ja creada.

2. **Provar les funcionalitats CRUD**:
   - **Veure clients**: La taula hauria de mostrar la llista de clients inicials.
   - **Afegir client**: Fes clic a "Agregar Client" per afegir un nou registre.
   - **Editar client**: Fes clic a "Editar" per modificar les dades d'un client.
   - **Eliminar client**: Fes clic a "Eliminar" per esborrar un client de la base de dades.

### Punts tècnics destacats (opcional per aprofundir)

- **Injecció de dependències**: Utilitzo `IHttpClientFactory` per gestionar les trucades HTTP des de `ERP.Web` cap a `ERP.Api` de forma segura i eficient.
- **Configuració centralitzada**: Tant la cadena de connexió com la URL de l'API estan configurades a `appsettings.json`, permetent més flexibilitat i una millor gestió entre entorns (dev/prod).
- **Bootstrap**: He aplicat estil de Bootstrap a la interfície per fer-la més intuïtiva i visualment atractiva.

Amb aquests passos, podràs executar i provar la solució completa sense problemes. Si tens qualsevol dubte durant la configuració, estaré encantat d'ajudar-te!

--- 