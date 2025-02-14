using System;
using System.Collections.Generic;
using Vintagestory.API.Server;

namespace AFKModule.Modules;

public static class Events
{
    public static readonly List<string> playersSoftAfk = [];
    public static readonly List<string> playersFullAfk = [];

    private static readonly Dictionary<string, List<string>> modulesSoftAFK = [];
    private static readonly Dictionary<string, List<string>> modulesFullAFK = [];

    #region Camera
    public static event EventHandler OnSoftAFKCamera;
    internal static void InvokeOnSoftAFKCamera(IServerPlayer player)
    {
        OnSoftAFKCamera?.Invoke(player, null);

        // Add it to the modules
        if (modulesSoftAFK.TryGetValue(player.PlayerUID, out List<string> modules))
        {
            if (!modules.Contains("Camera")) modulesSoftAFK[player.PlayerUID].Add("Camera");
        }
        else modulesSoftAFK.Add(player.PlayerUID, ["Camera"]);

        // Global afk system
        if (!playersSoftAfk.Contains(player.PlayerUID))
            playersSoftAfk.Add(player.PlayerUID);
    }

    public static event EventHandler OnFullAFKCamera;
    internal static void InvokeOnFullAFKCamera(IServerPlayer player)
    {
        OnFullAFKCamera?.Invoke(player, null);

        // Add it to the modules
        if (modulesFullAFK.TryGetValue(player.PlayerUID, out List<string> modules))
        {
            if (!modules.Contains("Camera")) modulesFullAFK[player.PlayerUID].Add("Camera");
        }
        else modulesFullAFK.Add(player.PlayerUID, ["Camera"]);

        // Global afk system
        if (!playersFullAfk.Contains(player.PlayerUID))
            playersFullAfk.Add(player.PlayerUID);
    }

    public static event EventHandler ExitSoftAFKCamera;
    internal static void InvokeExitSoftAFKCamera(IServerPlayer player)
    {
        ExitSoftAFKCamera?.Invoke(player, null);

        // Remove module
        if (modulesSoftAFK.TryGetValue(player.PlayerUID, out List<string> modules))
        {
            if (modules.Contains("Camera")) modulesSoftAFK[player.PlayerUID].Remove("Camera");
            if (modulesSoftAFK[player.PlayerUID].Count == 0)
            {

                modulesSoftAFK.Remove(player.PlayerUID);
                playersSoftAfk.Remove(player.PlayerUID);
            }
        }
        else playersSoftAfk.Remove(player.PlayerUID);
    }

    public static event EventHandler ExitFullAFKCamera;
    internal static void InvokeExitFullAFKCamera(IServerPlayer player)
    {
        ExitFullAFKCamera?.Invoke(player, null);

        // Remove module
        if (modulesFullAFK.TryGetValue(player.PlayerUID, out List<string> modules))
        {
            if (modules.Contains("Camera")) modulesFullAFK[player.PlayerUID].Remove("Camera");
            if (modulesFullAFK[player.PlayerUID].Count == 0)
            {
                modulesFullAFK.Remove(player.PlayerUID);
                playersFullAfk.Remove(player.PlayerUID);
            }
        }
        else playersFullAfk.Remove(player.PlayerUID);
    }
    #endregion

    #region Moviment
    public static event EventHandler OnSoftAFKMoviment;
    internal static void InvokeOnSoftAFKMoviment(IServerPlayer player)
    {
        OnSoftAFKMoviment?.Invoke(player, null);

        // Add it to the modules
        if (modulesSoftAFK.TryGetValue(player.PlayerUID, out List<string> modules))
        {
            if (!modules.Contains("Moviment")) modulesSoftAFK[player.PlayerUID].Add("Moviment");
        }
        else modulesSoftAFK.Add(player.PlayerUID, ["Moviment"]);

        // Global afk system
        if (!playersSoftAfk.Contains(player.PlayerUID))
            playersSoftAfk.Add(player.PlayerUID);
    }

    public static event EventHandler OnFullAFKMoviment;
    internal static void InvokeOnFullAFKMoviment(IServerPlayer player)
    {
        OnFullAFKMoviment?.Invoke(player, null);

        // Add it to the modules
        if (modulesFullAFK.TryGetValue(player.PlayerUID, out List<string> modules))
        {
            if (!modules.Contains("Moviment")) modulesFullAFK[player.PlayerUID].Add("Moviment");
        }
        else modulesFullAFK.Add(player.PlayerUID, ["Moviment"]);

        // Global afk system
        if (!playersFullAfk.Contains(player.PlayerUID))
            playersFullAfk.Add(player.PlayerUID);
    }

    public static event EventHandler ExitSoftAFKMoviment;
    internal static void InvokeExitSoftAFKMoviment(IServerPlayer player)
    {
        ExitSoftAFKMoviment?.Invoke(player, null);

        // Remove module
        if (modulesSoftAFK.TryGetValue(player.PlayerUID, out List<string> modules))
        {
            if (modules.Contains("Moviment")) modulesSoftAFK[player.PlayerUID].Remove("Moviment");
            if (modulesSoftAFK[player.PlayerUID].Count == 0)
            {
                modulesSoftAFK.Remove(player.PlayerUID);
                playersSoftAfk.Remove(player.PlayerUID);
            }
        }
        else playersFullAfk.Remove(player.PlayerUID);
    }

    public static event EventHandler ExitFullAFKMoviment;
    internal static void InvokeExitFullAFKMoviment(IServerPlayer player)
    {
        ExitFullAFKMoviment?.Invoke(player, null);

        // Remove module
        if (modulesFullAFK.TryGetValue(player.PlayerUID, out List<string> modules))
        {
            if (modules.Contains("Moviment")) modulesFullAFK[player.PlayerUID].Remove("Moviment");
            if (modulesFullAFK[player.PlayerUID].Count == 0)
            {
                modulesFullAFK.Remove(player.PlayerUID);
                playersFullAfk.Remove(player.PlayerUID);
            }
        }
        else playersFullAfk.Remove(player.PlayerUID);
    }
    #endregion

    #region Death
    public static event EventHandler OnSoftAFKDeath;
    internal static void InvokeOnSoftAFKDeath(IServerPlayer player)
    {
        OnSoftAFKDeath?.Invoke(player, null);

        // Add it to the modules
        if (modulesSoftAFK.TryGetValue(player.PlayerUID, out List<string> modules))
        {
            if (!modules.Contains("Death")) modulesSoftAFK[player.PlayerUID].Add("Death");
        }
        else modulesSoftAFK.Add(player.PlayerUID, ["Death"]);

        // Global afk system
        if (!playersSoftAfk.Contains(player.PlayerUID))
            playersSoftAfk.Add(player.PlayerUID);
    }

    public static event EventHandler OnFullAFKDeath;
    internal static void InvokeOnFullAFKDeath(IServerPlayer player)
    {
        OnFullAFKDeath?.Invoke(player, null);

        // Add it to the modules
        if (modulesFullAFK.TryGetValue(player.PlayerUID, out List<string> modules))
        {
            if (!modules.Contains("Death")) modulesFullAFK[player.PlayerUID].Add("Death");
        }
        else modulesFullAFK.Add(player.PlayerUID, ["Death"]);

        // Global afk system
        if (!playersFullAfk.Contains(player.PlayerUID))
            playersFullAfk.Add(player.PlayerUID);
    }

    public static event EventHandler ExitSoftAFKDeath;
    internal static void InvokeExitSoftAFKDeath(IServerPlayer player)
    {
        ExitSoftAFKDeath?.Invoke(player, null);

        // Remove module
        if (modulesSoftAFK.TryGetValue(player.PlayerUID, out List<string> modules))
        {
            if (modules.Contains("Death")) modulesSoftAFK[player.PlayerUID].Remove("Death");
            if (modulesSoftAFK[player.PlayerUID].Count == 0)
            {
                modulesSoftAFK.Remove(player.PlayerUID);
                playersSoftAfk.Remove(player.PlayerUID);
            }
        }
        else playersFullAfk.Remove(player.PlayerUID);
    }

    public static event EventHandler ExitFullAFKDeath;
    internal static void InvokeExitFullAFKDeath(IServerPlayer player)
    {
        ExitFullAFKDeath?.Invoke(player, null);

        // Remove module
        if (modulesFullAFK.TryGetValue(player.PlayerUID, out List<string> modules))
        {
            if (modules.Contains("Death")) modulesFullAFK[player.PlayerUID].Remove("Death");
            if (modulesFullAFK[player.PlayerUID].Count == 0)
            {
                modulesFullAFK.Remove(player.PlayerUID);
                playersFullAfk.Remove(player.PlayerUID);
            }
        }
        else playersFullAfk.Remove(player.PlayerUID);
    }
    #endregion
}