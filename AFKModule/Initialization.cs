using System;
using System.Collections.Generic;
using AFKModule.Modules;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace AFKModule;

public class Initialization : ModSystem
{
    public static readonly Dictionary<string, List<object>> playersAFKObject = [];

    private ICoreServerAPI serverAPI;

    public override void StartServerSide(ICoreServerAPI api)
    {
        base.StartServerSide(api);
        serverAPI = api;

        api.Event.RegisterGameTickListener(TickrateModules, Configuration.millisecondsPerModule);
        api.Event.PlayerNowPlaying += PlayerJoined;
        api.Event.PlayerDisconnect += PlayerDisconnected;

        if (Configuration.kickWhenFullAfk)
        {
            Events.OnFullAFKCamera += KickAFKPlayer;
            Events.OnFullAFKMoviment += KickAFKPlayer;
            Events.OnFullAFKDeath += KickAFKPlayer;
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
        // Disposing modules
        foreach (var dictionary in playersAFKObject)
        {
            if (dictionary.Key != byPlayer.PlayerUID) continue;

            List<object> modules = dictionary.Value;
            foreach (object module in modules)
            {
                switch (module.GetType().Name)
                {
                    case "DeathAFK":
                        (module as DeathAFK).Dispose();
                        break;
                }
            }
            break;
        }

        playersAFKObject.Remove(byPlayer.PlayerUID);
        Events.playersFullAfk.Remove(byPlayer.PlayerUID);
        Events.playersSoftAfk.Remove(byPlayer.PlayerUID);

        GC.Collect();
    }

    private void PlayerJoined(IServerPlayer byPlayer)
    {
        List<object> modules = [];

        if (Configuration.enableModuleMoviment)
        {
            var module = new MovimentAFK(byPlayer);
            if (Configuration.playerJoinInSoftAFKState)
            {
                module.actualthreshold = Configuration.softAfkThresholdCamera;
                module.softAfk = true;
            }

            modules.Add(module);
        }
        if (Configuration.enableModuleCamera)
        {
            var module = new CameraAFK(byPlayer);
            if (Configuration.playerJoinInSoftAFKState)
            {
                module.actualthreshold = Configuration.softAfkThresholdMoviment;
                module.softAfk = true;
            }

            modules.Add(module);
        }

        if (Configuration.enableModuleDeath)
        {
            var module = new DeathAFK(byPlayer, serverAPI.Event);
            if (Configuration.playerJoinInSoftAFKState)
            {
                module.actualthreshold = Configuration.softAfkThresholdDeath;
                module.softAfk = true;
            }
            modules.Add(module);
        }


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
                    case "DeathAFK":
                        (module as DeathAFK).OnTick();
                        break;
                }
            }
        }
    }
}

public class Debug
{
    static private ILogger loggerForNonTerminalUsers;

    static public void LoadLogger(ILogger logger) => loggerForNonTerminalUsers = logger;
    static public void Log(string message)
        => loggerForNonTerminalUsers?.Log(EnumLogType.Notification, $"[AFKModule] {message}");
    
}