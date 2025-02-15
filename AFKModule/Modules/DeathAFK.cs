using System;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace AFKModule.Modules;

public class DeathAFK
{
    private readonly IServerPlayer _player;
    private readonly IServerEventAPI _events;

    public bool softAfk = false;
    public bool fullAfk = false;
    public bool stillDead = false;
    public bool botAlert = false;

    public int actualthreshold = 0;
    public int timesDiedInThisModule = 0;

    private long previouslyTimestamp = 0;

    public DeathAFK(IServerPlayer player, IServerEventAPI events)
    {
        previouslyTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        _player = player;
        _events = events;

        if (!_player.Entity.Alive)
        {
            softAfk = true;
            stillDead = true;
            timesDiedInThisModule++;
            actualthreshold = Configuration.softAfkThresholdDeath;
            Events.InvokeOnSoftAFKDeath(_player);
        }

        _events.PlayerDeath += OnPlayerDead;
        _events.PlayerRespawn += OnPlayerRespawn;
    }

    public void OnTick()
    {
        if (!stillDead) return;

        actualthreshold++;

        if (actualthreshold > Configuration.softAfkThresholdDeath)
        {
            if (!softAfk)
            {
                softAfk = true;
                Events.InvokeOnSoftAFKDeath(_player);
            }
        }
        else
        {
            if (softAfk)
            {
                softAfk = false;
                Events.InvokeExitSoftAFKDeath(_player);
            }
        }
        if (actualthreshold > Configuration.fullAfkThresholdDeath)
        {
            if (!fullAfk)
            {
                fullAfk = true;
                Events.InvokeOnFullAFKDeath(_player);
            }
        }
        else
        {
            if (fullAfk)
            {
                fullAfk = false;
                Events.InvokeExitFullAFKDeath(_player);
            }
        }

        if(Configuration.enableExtendedLog)
            Debug.Log($"[Death] {_player.PlayerName}: Death Threshold: {actualthreshold}");
    }

    private void OnPlayerRespawn(IServerPlayer byPlayer)
    {
        if (byPlayer.PlayerUID != _player.PlayerUID) return;

        if (actualthreshold > Configuration.softAfkThresholdDeath)
        {
            softAfk = false;
            Events.InvokeExitSoftAFKDeath(_player);
        }
        if (actualthreshold > Configuration.fullAfkThresholdDeath)
        {
            fullAfk = false;
            Events.InvokeExitFullAFKDeath(_player);
        }

        actualthreshold = 0;
        stillDead = false;
    }

    private void OnPlayerDead(IServerPlayer byPlayer, DamageSource _)
    {
        if (byPlayer.PlayerUID != _player.PlayerUID) return;

        timesDiedInThisModule++;
        actualthreshold = 0;

        stillDead = true;

        #region bot check
        if (timesDiedInThisModule > Configuration.botAlertMaxDeathsPerModule)
            Debug.Log($"BOTALERT: {_player.PlayerName} died too fast!!, coords: X:{_player.Entity.Pos.X} Y:{_player.Entity.Pos.Y} Z:{_player.Entity.Pos.Z}");


        long secondsPassedPreviouslyDeath = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - previouslyTimestamp;
        if (secondsPassedPreviouslyDeath > Configuration.reduceDeathCountModulePerSecond)
        {
            timesDiedInThisModule--;
            previouslyTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
        #endregion
    }

    public void Dispose()
    {
        _events.PlayerDeath -= OnPlayerDead;
        _events.PlayerRespawn -= OnPlayerRespawn;
    }
}