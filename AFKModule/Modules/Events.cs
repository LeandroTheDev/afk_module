using System;
using System.Collections.Generic;
using Vintagestory.API.Server;

namespace AFKModule.Modules;

public static class Events
{
    public static readonly List<string> playersSoftAfk = [];
    public static readonly List<string> playersFullAfk = [];

    #region Camera
    public static event EventHandler OnSoftAFKCamera;
    internal static void InvokeOnSoftAFKCamera(IServerPlayer player)
    {
        OnSoftAFKCamera?.Invoke(player, null);
        if (!playersSoftAfk.Contains(player.PlayerUID))
            playersSoftAfk.Add(player.PlayerUID);
    }

    public static event EventHandler OnFullAFKCamera;
    internal static void InvokeOnFullAFKCamera(IServerPlayer player)
    {
        OnFullAFKCamera?.Invoke(player, null);
        if (!playersFullAfk.Contains(player.PlayerUID))
            playersFullAfk.Add(player.PlayerUID);
    }

    public static event EventHandler ExitSoftAFKCamera;
    internal static void InvokeExitSoftAFKCamera(IServerPlayer player)
    {
        ExitSoftAFKCamera?.Invoke(player, null);
        if (!playersSoftAfk.Contains(player.PlayerUID))
            playersSoftAfk.Add(player.PlayerUID);
    }

    public static event EventHandler ExitFullAFKCamera;
    internal static void InvokeExitFullAFKCamera(IServerPlayer player)
    {
        ExitFullAFKCamera?.Invoke(player, null);
        if (!playersFullAfk.Contains(player.PlayerUID))
            playersFullAfk.Add(player.PlayerUID);
    }
    #endregion

    #region Moviment
    public static event EventHandler OnSoftAFKMoviment;
    internal static void InvokeOnSoftAFKMoviment(IServerPlayer player)
    {
        OnSoftAFKMoviment?.Invoke(player, null);
        if (!playersSoftAfk.Contains(player.PlayerUID))
            playersSoftAfk.Add(player.PlayerUID);
    }

    public static event EventHandler OnFullAFKMoviment;
    internal static void InvokeOnFullAFKMoviment(IServerPlayer player)
    {
        OnFullAFKMoviment?.Invoke(player, null);
        if (!playersFullAfk.Contains(player.PlayerUID))
            playersFullAfk.Add(player.PlayerUID);
    }

    public static event EventHandler ExitSoftAFKMoviment;
    internal static void InvokeExitSoftAFKMoviment(IServerPlayer player)
    {
        ExitSoftAFKMoviment?.Invoke(player, null);
        if (!playersSoftAfk.Contains(player.PlayerUID))
            playersSoftAfk.Add(player.PlayerUID);
    }

    public static event EventHandler ExitFullAFKMoviment;
    internal static void InvokeExitFullAFKMoviment(IServerPlayer player)
    {
        ExitFullAFKMoviment?.Invoke(player, null);
        if (!playersFullAfk.Contains(player.PlayerUID))
            playersFullAfk.Add(player.PlayerUID);
    }
    #endregion
}