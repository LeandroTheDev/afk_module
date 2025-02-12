using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace AFKModule.Modules;

public class CameraAFK(IServerPlayer player)
{
    private readonly IServerPlayer _player = player;

    public bool softAfk = false;
    public bool fullAfk = false;

    public int actualthreshold = 0;
    private float lastYaw = 0.0f;
    private float lastPitch = 0.0f;

    public void OnTick()
    {
        bool yawOk = false;
        bool pitchOk = false;

        #region Yaw Check
        float yawDifference;
        if (_player.Entity.SidedPos.Yaw <= lastYaw) yawDifference = lastYaw - _player.Entity.SidedPos.Yaw;
        else yawDifference = _player.Entity.SidedPos.Yaw - lastYaw;

        if (yawDifference < Configuration.lastYawDistanceToCountAsThresholdCamera) actualthreshold++;
        else yawOk = true;
        #endregion

        #region Pitch Check
        float pitchDifference;
        if (_player.Entity.SidedPos.Pitch <= lastPitch) pitchDifference = lastPitch - _player.Entity.SidedPos.Pitch;
        else pitchDifference = _player.Entity.SidedPos.Pitch - lastPitch;

        if (pitchDifference < Configuration.lastPitchDistanceToCountAsThresholdCamera) actualthreshold++;
        else pitchOk = true;
        #endregion

        if (pitchOk && yawOk) actualthreshold = 0;

        if (actualthreshold > Configuration.softAfkThresholdCamera)
        {
            if (!softAfk)
            {
                softAfk = true;
                Events.InvokeOnSoftAFKCamera(_player);
            }
        }
        else
        {
            if (softAfk)
            {
                softAfk = false;
                Events.InvokeExitSoftAFKCamera(_player);
            }
        }
        if (actualthreshold > Configuration.fullAfkThresholdCamera)
        {
            if (!fullAfk)
            {
                fullAfk = true;
                Events.InvokeOnFullAFKCamera(_player);
            }
        }
        else
        {
            if (fullAfk)
            {
                fullAfk = false;
                Events.InvokeExitFullAFKCamera(_player);
            }
        }

        lastYaw = _player.Entity.SidedPos.Yaw;
        lastPitch = _player.Entity.SidedPos.Pitch;

        if (Configuration.enableExtendedLog)
            Debug.Log($"[Camera] {_player.PlayerName}: P/Y, {pitchDifference}/{yawDifference}");
    }
}