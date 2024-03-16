List<IMyShipController> cryoChambers = new List<IMyShipController>();
List<IMyTextPanel> lcdPanels = new List<IMyTextPanel>();

private double elapsedSeconds = 0;

public Program()
{
    GridTerminalSystem.GetBlocksOfType<IMyShipController>(cryoChambers, x => x.CustomName.Contains("CryoChamber"));
    GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(lcdPanels, x => x.CustomName.Contains("CryoStatusPanel"));

    Echo($"Se encontraron {cryoChambers.Count} cámaras criogénicas y {lcdPanels.Count} paneles LCD.");

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
    elapsedSeconds += Runtime.TimeSinceLastRun.TotalSeconds;

    if (elapsedSeconds < 10)
    {
        return;
    }

    elapsedSeconds = 0;

    foreach (var cryoChamber in cryoChambers)
    {
        bool isOccupied = cryoChamber.IsUnderControl;

        Echo($"{cryoChamber.CustomName} está {(isOccupied ? "ocupada" : "libre")}.");

        var lcdPanel = lcdPanels.FirstOrDefault(x => x.CustomName.Replace("CryoStatusPanel", "") == cryoChamber.CustomName.Replace("CryoChamber", ""));

        if (lcdPanel != null)
        {
            lcdPanel.BackgroundColor = isOccupied ? new Color(255, 0, 0) : new Color(0, 255, 0);
            lcdPanel.FontColor = new Color(0, 0, 0);

            lcdPanel.WriteText($"{(isOccupied ? "ocupada" : "libre")}.");

            Echo($"El panel LCD {lcdPanel.CustomName} tiene un color de fondo {(isOccupied ? "rojo" : "verde")}.");
        }
        else
        {
            Echo($"No se encontró un panel LCD correspondiente para {cryoChamber.CustomName}.");
        }
    }
}