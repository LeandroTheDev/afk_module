using System;
using Vintagestory.API.Server;

namespace AFKModule.Modules;

public static class Events
{
    #region Camera
    public static event EventHandler OnSoftAFKCamera;
    internal static void InvokeOnSoftAFKCamera(IServerPlayer player) => OnSoftAFKCamera.Invoke(player, null);

    public static event EventHandler OnFullAFKCamera;
    internal static void InvokeOnFullAFKCamera(IServerPlayer player) => OnFullAFKCamera.Invoke(player, null);

    public static event EventHandler ExitSoftAFKCamera;
    internal static void InvokeExitSoftAFKCamera(IServerPlayer player) => ExitSoftAFKCamera.Invoke(player, null);

    public static event EventHandler ExitFullAFKCamera;
    internal static void InvokeExitFullAFKCamera(IServerPlayer player) => ExitFullAFKCamera.Invoke(player, null);
    #endregion

    #region Moviment
    public static event EventHandler OnSoftAFKMoviment;
    internal static void InvokeOnSoftAFKMoviment(IServerPlayer player) => OnSoftAFKMoviment.Invoke(player, null);

    public static event EventHandler OnFullAFKMoviment;
    internal static void InvokeOnFullAFKMoviment(IServerPlayer player) => OnFullAFKMoviment.Invoke(player, null);

    public static event EventHandler ExitSoftAFKMoviment;
    internal static void InvokeExitSoftAFKMoviment(IServerPlayer player) => ExitSoftAFKMoviment.Invoke(player, null);

    public static event EventHandler ExitFullAFKMoviment;
    internal static void InvokeExitFullAFKMoviment(IServerPlayer player) => ExitFullAFKMoviment.Invoke(player, null);
    #endregion
}