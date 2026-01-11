# Delivery 3: In-editor visualization
The aim of this delivery was to create a data visualization toolkit inside Unity that allows gameplay events to be inspected directly in the editor, while
also providing ways to interact with and filter the data and secondly, to create a pipeline where we can export said data to our server using php, MySQL and fileZilla.
### OUR TEAM
Our team consists of, [Sofia Liles](https://github.com/Sofialiles55),[Pau Vives](https://github.com/Paules23), [Duarte Olindo](https://github.com/duarteolindo7) and [Lihan Yu](https://github.com/gioeverni)
### Events Collected

Our system records different event types:

- Path – player movement samples (used to generate heatmaps)

- Player Path Heatmap (Player movement is recorded as a sequence of path events)

- Death – player deaths and respawns

- Switches – activation of switches 

- Popups or text – tutorial or text popup triggers

- Enemy deaths – pretty self explanatory 

### How to use the toolkit in Unity 

- Download the project and go to Scenes/Entrega3Scenes/DataVisualizationScene and open it
- Click the DataVisualization object under Entrega 3 in the hierarchy which will allow you to see the data in the scene.
- Press play and enjoy! You'll see the data show up before your very eyes!
- When you are done you can go to the event logger and upload or download the data from any game you have played, OR all the games to study trends.
- To see our scripts, go to Entrega3 scripts in assets

### Server Connection (Unity to PHP to MySQL)
This project includes a complete pipeline that connects a Unity game to a remote MySQL database using PHP scripts hosted on our server.
The system enables persistent gameplay analytics and visualization by allowing data to flow from Unity → Server → Unity.

### Features
- Log gameplay events in Unity
- Upload data to our server
- Store and filter events in our MySQL database
- Download stored data back into Unity
- Visualize the data in unity scene
- Customize the data visualization by color and size
- Choose what kind of data you want to see with toggles
  







