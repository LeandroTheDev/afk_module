using Vintagestory.API.Common.Entities;
using Vintagestory.API.Server;

namespace AFKModule.Modules;

public class MovimentAFK(IServerPlayer player)
{
    private readonly IServerPlayer _player = player;

    public bool softAfk = false;
    public bool fullAfk = false;

    public int actualthreshold = 0;
    private EntityPos lastPosition = new();

    public void OnTick()
    {
        double tickDistance = _player.Entity.SidedPos.DistanceTo(lastPosition);
        if (tickDistance < Configuration.lastDistanceToCountAsThresholdMoviment) actualthreshold++;
        else actualthreshold = 0;


        if (actualthreshold > Configuration.softAfkThresholdMoviment)
        {
            if (!softAfk)
            {
                softAfk = true;
                Events.InvokeOnSoftAFKMoviment(_player);
            }
        }
        else
        {
            if (softAfk)
            {                
                softAfk = false;
                Events.InvokeExitSoftAFKMoviment(_player);
            }
        }
        if (actualthreshold > Configuration.fullAfkThresholdMoviment)
        {
            if (!fullAfk)
            {
                fullAfk = true;
                Events.InvokeOnFullAFKMoviment(_player);
            }
        }
        else
        {
            if (fullAfk)
            {
                fullAfk = false;
                Events.InvokeExitFullAFKMoviment(_player);
            }
        }

        lastPosition = _player.Entity.Pos.Copy();

        if (Configuration.enableExtendedLog)
            Debug.Log($"[Moviment] {_player.PlayerName}: {tickDistance}, Threshold: {actualthreshold}");
    }
}