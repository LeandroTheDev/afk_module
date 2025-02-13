using System;
using System.Collections.Generic;
using AFKModule.Modules;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace AFKModule;

public class Initialization : ModSystem
{
    public static readonly Dictionary<string, List<object>> playersAFKObject = [];

    public override void StartServerSide(ICoreServerAPI api)
    {
        base.StartServerSide(api);
        api.Event.RegisterGameTickListener(TickrateModules, Configuration.millisecondsPerModule);
        api.Event.PlayerNowPlaying += PlayerJoined;
        api.Event.PlayerDisconnect += PlayerDisconnected;

        if (Configuration.kickWhenFullAfk)
        {
            Events.OnFullAFKCamera += KickAFKPlayer;
            Events.OnFullAFKMoviment += KickAFKPlayer;
        }
        Debug.Log("Events created");
    }


    private void KickAFKPlayer(object sender, EventArgs e)
        => (sender as IServerPlayer).Disconnect(Configuration.kickMessageToPlayerKicked);

    public override void AssetsLoaded(ICoreAPI api)
    {
        base.AssetsLoaded(api);
        Configuration.UpdateBaseConfigurations(api);
    }

    private void PlayerDisconnected(IServerPlayer byPlayer)
    {
        playersAFKObject.Remove(byPlayer.PlayerUID);
        Events.playersFullAfk.Remove(byPlayer.PlayerUID);
        Events.playersSoftAfk.Remove(byPlayer.PlayerUID);
    }

    private void PlayerJoined(IServerPlayer byPlayer)
    {
        List<object> modules = [];

        if (Configuration.enableModuleMoviment)
            modules.Add(new MovimentAFK(byPlayer));
        if (Configuration.enableModuleCamera)
            modules.Add(new CameraAFK(byPlayer));


        playersAFKObject.Add(byPlayer.PlayerUID, modules);
    }

    private void TickrateModules(float obj)
    {
        foreach (var dictionary in playersAFKObject)
        {
            string playerUID = dictionary.Key;
            List<object> modules = dictionary.Value;

            foreach (object module in modules)
            {
                switch (module.GetType().Name)
                {
                    case "MovimentAFK":
                        (module as MovimentAFK).OnTick();
                        break;
                    case "CameraAFK":
                        (module as CameraAFK).OnTick();
                        break;
                }
            }
        }
    }
}

public class Debug
{
    private static readonly OperatingSystem system = Environment.OSVersion;
    static private ILogger loggerForNonTerminalUsers;

    static public void LoadLogger(ILogger logger) => loggerForNonTerminalUsers = logger;
    static public void Log(string message)
    {
        // Check if is linux or other based system and if the terminal is active for the logs to be show
        if (system.Platform == PlatformID.Unix || system.Platform == PlatformID.Other || Environment.UserInteractive)
            // Based terminal users
            Console.WriteLine($"{DateTime.Now:d.M.yyyy HH:mm:ss} [AFKModule] {message}");
        else
            // Unbased non terminal users
            loggerForNonTerminalUsers?.Log(EnumLogType.Notification, $"[AFKModule] {message}");
    }
}