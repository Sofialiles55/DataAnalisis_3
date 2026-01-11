# Delivery 3: In-editor visualization
The aim of this delivery was to create a data visualization toolkit inside Unity that allows gameplay events to be inspected directly in the editor, while
also providing ways to interact with and filter the data and secondly, to create a pipeline where we can export said data to our server using php, MySQL and fileZilla.

# Server Connection (Unity ↔ PHP ↔ MySQL)
This project includes a complete telemetry pipeline that connects a Unity game to a remote MySQL database using PHP scripts hosted on a server.
The system enables persistent gameplay analytics and visualization by allowing data to flow from Unity → Server → Unity.

### Features
- Log gameplay events in Unity
- Upload telemetry data to a remote server
- Store and filter events in a MySQL database
- Download stored telemetry back into Unity
- Visualize data (player paths, heatmaps, etc.)

## PHP Files (Server Side)
All PHP files are hosted on the server and uploaded using FileZilla.

### 1. db.php (Database Connection)
Responsible for creating a secure connection to the MySQL database using PDO.

#### Responsibilities:
- Define database credentials (host, database name, user, password)
- Create a PDO instance with error handling
- Provide a shared database connection for all other PHP scripts

### 2. uploadEvents.php (Upload Telemetry from Unity)
This endpoint receives JSON data sent from Unity and inserts it into the telemetry_events table.

#### Used when:
- The game is running
- Events are logged and uploaded from Unity

#### Input (JSON):
- user_id
- session_id
- event_type
- x, y, z (world position)
- t (timestamp)
- meta (optional metadata)

#### Behavior:
- Validates JSON input
- Inserts a new row into the database
- Returns { "status": "ok" } on success

### 3. downloadEvents.php — Download Telemetry to Unity
This endpoint returns stored telemetry events as JSON.

#### Supports optional filters:
- user_id
- session_id
- event_type

#### Behavior:
- If session_id is provided → returns events from that session
- If session_id is omitted → returns events from all sessions

## Database
All telemetry is stored in the telemetry_events table, containing:
- user_id
- session_id
- event_type
- x, y, z
- t
- meta

#### This allows:
- SQL filtering
- Data inspection
- External analysis
- Re-importing data into Unity


# OUR TEAM
Our team consists of, [Sofia Liles](https://github.com/Sofialiles55),[Pau Vives](https://github.com/Paules23), [Duarte Olindo](https://github.com/duarteolindo7) and [Lihan Yu](https://github.com/gioeverni)


