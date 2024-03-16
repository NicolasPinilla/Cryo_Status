// Tags

// Nombre de las cámaras criogénicas
string cryoChamberName = "CryoChamber";
// Nombre de los paneles LCD
string lcdPanelName = "CryoStatusPanel";


List<IMyShipController> cryoChambers = new List<IMyShipController>();
List<IMyTextPanel> lcdPanels = new List<IMyTextPanel>();

// Contador para el retraso
private double elapsedSeconds = 0;

public Program()
{
    // Obtén todas las cámaras criogénicas y los paneles LCD en la red del barco
    GridTerminalSystem.GetBlocksOfType<IMyShipController>(cryoChambers, x => x.CustomName.Contains(cryoChamberName));
    GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(lcdPanels, x => x.CustomName.Contains(lcdPanelName));

    // Establece la frecuencia de actualización para que el código se ejecute cada segundo
    Runtime.UpdateFrequency = UpdateFrequency.Update1;

    foreach (var lcdPanel in lcdPanels)
    {
        lcdPanel.Font = "DejaVu Sans";
        lcdPanel.FontSize = 8;
        lcdPanel.Alignment = VRage.Game.GUI.TextPanel.TextAlignment.CENTER;
        lcdPanel.TextPadding = 0.1f;
        lcdPanel.ContentType = VRage.Game.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;
        lcdPanel.FontColor = new Color(0, 0, 0);
        lcdPanel.BackgroundColor = new Color(255, 255, 255);
        lcdPanel.WriteText("Cryo Status");
    }
}

public void Main(string argument, UpdateType updateSource)
{
    // Suma el tiempo transcurrido desde la última ejecución
    elapsedSeconds += Runtime.TimeSinceLastRun.TotalSeconds;

    // Verifica si han pasado 10 segundos
    if (elapsedSeconds < 10)
    {
        return;
    }

    // Resetea el contador de tiempo
    elapsedSeconds = 0;

    // Inicializar los contadores de cámaras criogénicas ocupadas y disponibles
    int occupiedCryoChambers = 0;
    int availableCryoChambers = 0;

    foreach (var cryoChamber in cryoChambers)
    {
        // Comprueba si la cámara criogénica está ocupada
        bool isOccupied = cryoChamber.IsUnderControl;

        // Incrementa el contador correspondiente
        if (isOccupied)
        {
            occupiedCryoChambers++;
        }
        else
        {
            availableCryoChambers++;
        }

        // Encuentra el panel LCD correspondiente
        var lcdPanel = lcdPanels.FirstOrDefault(x => x.CustomName.Replace(lcdPanelName, "") == cryoChamber.CustomName.Replace(cryoChamberName, ""));

        if (lcdPanel != null)
        {
            // Cambia el color de fondo del panel LCD en función de si la cámara criogénica está ocupada
            lcdPanel.BackgroundColor = isOccupied ? new Color(255, 0, 0) : new Color(0, 255, 0);
            lcdPanel.FontColor = new Color(0, 0, 0);

            // Actualiza el texto del panel LCD
            lcdPanel.WriteText(" ");
        }
    }

    // Crear el resumen
    string summary = $"Se encontraron {cryoChambers.Count} cámaras criogénicas y {lcdPanels.Count} paneles LCD.\n\nNúmero total de cámaras criogénicas ocupadas: {occupiedCryoChambers}\n\nNúmero total de cámaras criogénicas disponibles: {availableCryoChambers}";

    // Imprimir el resumen en el log
    Echo(summary);

    // Obtener la pantalla del bloque programable
    var surface = Me.GetSurface(0);

    // Cambiar el tamaño de la fuente
    surface.FontSize = 0.5f;

    // Mostrar el resumen en la pantalla del bloque programable
    surface.WriteText(summary);
}