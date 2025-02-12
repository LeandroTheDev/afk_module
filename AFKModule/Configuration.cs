using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Vintagestory.API.Common;

namespace AFKModule;

#pragma warning disable CA2211
public static class Configuration
{
    private static Dictionary<string, object> LoadConfigurationByDirectoryAndName(ICoreAPI api, string directory, string name, string defaultDirectory)
    {
        string directoryPath = Path.Combine(api.DataBasePath, directory);
        string configPath = Path.Combine(api.DataBasePath, directory, $"{name}.json");
        Dictionary<string, object> loadedConfig;
        try
        {
            // Load server configurations
            string jsonConfig = File.ReadAllText(configPath);
            loadedConfig = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonConfig);
        }
        catch (DirectoryNotFoundException)
        {
            Debug.Log($"WARNING: Server configurations directory does not exist creating {name}.json and directory...");
            try
            {
                Directory.CreateDirectory(directoryPath);
            }
            catch (Exception ex)
            {
                Debug.Log($"ERROR: Cannot create directory: {ex.Message}");
            }
            Debug.Log("Loading default configurations...");
            // Load default configurations
            loadedConfig = api.Assets.Get(new AssetLocation(defaultDirectory)).ToObject<Dictionary<string, object>>();

            Debug.Log($"Configurations loaded, saving configs in: {configPath}");
            try
            {
                // Saving default configurations
                string defaultJson = JsonConvert.SerializeObject(loadedConfig, Formatting.Indented);
                File.WriteAllText(configPath, defaultJson);
            }
            catch (Exception ex)
            {
                Debug.Log($"ERROR: Cannot save default files to {configPath}, reason: {ex.Message}");
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log($"WARNING: Server configurations {name}.json cannot be found, recreating file from default");
            Debug.Log("Loading default configurations...");
            // Load default configurations
            loadedConfig = api.Assets.Get(new AssetLocation(defaultDirectory)).ToObject<Dictionary<string, object>>();

            Debug.Log($"Configurations loaded, saving configs in: {configPath}");
            try
            {
                // Saving default configurations
                string defaultJson = JsonConvert.SerializeObject(loadedConfig, Formatting.Indented);
                File.WriteAllText(configPath, defaultJson);
            }
            catch (Exception ex)
            {
                Debug.Log($"ERROR: Cannot save default files to {configPath}, reason: {ex.Message}");
            }

        }
        catch (Exception ex)
        {
            Debug.Log($"ERROR: Cannot read the server configurations: {ex.Message}");
            Debug.Log("Loading default values from mod assets...");
            // Load default configurations
            loadedConfig = api.Assets.Get(new AssetLocation(defaultDirectory)).ToObject<Dictionary<string, object>>();
        }
        return loadedConfig;
    }


    #region baseconfigs
    public static double millisecondsPerModule = 0.5;

    #region Module Moviment
    public static bool enableModuleMoviment = true;
    public static int softAfkThresholdMoviment = 10;
    public static int fullAfkThresholdMoviment = 200;
    public static double lastDistanceToCountAsThresholdMoviment = 1.0;
    #endregion
    #region Module Camera
    public static bool enableModuleCamera = true;
    public static int softAfkThresholdCamera = 10;
    public static int fullAfkThresholdCamera = 200;
    public static double lastYawDistanceToCountAsThresholdCamera = 1.0;
    public static double lastPitchDistanceToCountAsThresholdCamera = 1.0;
    #endregion
    public static bool kickWhenFullAfk = true;
    public static string kickMessageToPlayerKicked = "";
    public static bool enableExtendedLog = false;

    public static void UpdateBaseConfigurations(ICoreAPI api)
    {
        Dictionary<string, object> baseConfigs = LoadConfigurationByDirectoryAndName(
            api,
            "ModConfig/AFKModule/config",
            "base",
            "afkmodule:config/base.json"
        );
        { //millisecondsPerModule
            if (baseConfigs.TryGetValue("millisecondsPerModule", out object value))
                if (value is null) Debug.Log("CONFIGURATION ERROR: millisecondsPerModule is null");
                else if (value is not long) Debug.Log($"CONFIGURATION ERROR: millisecondsPerModule is not int is {value.GetType()}");
                else millisecondsPerModule = (int)(long)value;
            else Debug.Log("CONFIGURATION ERROR: millisecondsPerModule not set");
        }
        { //enableModuleMoviment
            if (baseConfigs.TryGetValue("enableModuleMoviment", out object value))
                if (value is null) Debug.Log("CONFIGURATION ERROR: enableModuleMoviment is null");
                else if (value is not bool) Debug.Log($"CONFIGURATION ERROR: enableModuleMoviment is not boolean is {value.GetType()}");
                else enableModuleMoviment = (bool)value;
            else Debug.Log("CONFIGURATION ERROR: enableModuleMoviment not set");
        }
        { //softAfkThresholdMoviment
            if (baseConfigs.TryGetValue("softAfkThresholdMoviment", out object value))
                if (value is null) Debug.Log("CONFIGURATION ERROR: softAfkThresholdMoviment is null");
                else if (value is not long) Debug.Log($"CONFIGURATION ERROR: softAfkThresholdMoviment is not int is {value.GetType()}");
                else softAfkThresholdMoviment = (int)(long)value;
            else Debug.Log("CONFIGURATION ERROR: softAfkThresholdMoviment not set");
        }
        { //fullAfkThresholdMoviment
            if (baseConfigs.TryGetValue("fullAfkThresholdMoviment", out object value))
                if (value is null) Debug.Log("CONFIGURATION ERROR: fullAfkThresholdMoviment is null");
                else if (value is not long) Debug.Log($"CONFIGURATION ERROR: fullAfkThresholdMoviment is not int is {value.GetType()}");
                else fullAfkThresholdMoviment = (int)(long)value;
            else Debug.Log("CONFIGURATION ERROR: fullAfkThresholdMoviment not set");
        }
        { //lastDistanceToCountAsThresholdMoviment
            if (baseConfigs.TryGetValue("lastDistanceToCountAsThresholdMoviment", out object value))
                if (value is null) Debug.Log("CONFIGURATION ERROR: lastDistanceToCountAsThresholdMoviment is null");
                else if (value is not double) Debug.Log($"CONFIGURATION ERROR: lastDistanceToCountAsThresholdMoviment is not double is {value.GetType()}");
                else lastDistanceToCountAsThresholdMoviment = (double)value;
            else Debug.Log("CONFIGURATION ERROR: lastDistanceToCountAsThresholdMoviment not set");
        }
        { //enableModuleCamera
            if (baseConfigs.TryGetValue("enableModuleCamera", out object value))
                if (value is null) Debug.Log("CONFIGURATION ERROR: enableModuleCamera is null");
                else if (value is not bool) Debug.Log($"CONFIGURATION ERROR: enableModuleCamera is not boolean is {value.GetType()}");
                else enableModuleCamera = (bool)value;
            else Debug.Log("CONFIGURATION ERROR: enableModuleCamera not set");
        }
        { //softAfkThresholdCamera
            if (baseConfigs.TryGetValue("softAfkThresholdCamera", out object value))
                if (value is null) Debug.Log("CONFIGURATION ERROR: softAfkThresholdCamera is null");
                else if (value is not long) Debug.Log($"CONFIGURATION ERROR: softAfkThresholdCamera is not int is {value.GetType()}");
                else softAfkThresholdCamera = (int)(long)value;
            else Debug.Log("CONFIGURATION ERROR: softAfkThresholdCamera not set");
        }
        { //fullAfkThresholdCamera
            if (baseConfigs.TryGetValue("fullAfkThresholdCamera", out object value))
                if (value is null) Debug.Log("CONFIGURATION ERROR: fullAfkThresholdCamera is null");
                else if (value is not long) Debug.Log($"CONFIGURATION ERROR: fullAfkThresholdCamera is not int is {value.GetType()}");
                else fullAfkThresholdCamera = (int)(long)value;
            else Debug.Log("CONFIGURATION ERROR: fullAfkThresholdCamera not set");
        }
        { //lastYawDistanceToCountAsThresholdCamera
            if (baseConfigs.TryGetValue("lastYawDistanceToCountAsThresholdCamera", out object value))
                if (value is null) Debug.Log("CONFIGURATION ERROR: lastYawDistanceToCountAsThresholdCamera is null");
                else if (value is not double) Debug.Log($"CONFIGURATION ERROR: lastYawDistanceToCountAsThresholdCamera is not double is {value.GetType()}");
                else lastYawDistanceToCountAsThresholdCamera = (double)value;
            else Debug.Log("CONFIGURATION ERROR: lastYawDistanceToCountAsThresholdCamera not set");
        }
        { //lastPitchDistanceToCountAsThresholdCamera
            if (baseConfigs.TryGetValue("lastPitchDistanceToCountAsThresholdCamera", out object value))
                if (value is null) Debug.Log("CONFIGURATION ERROR: lastPitchDistanceToCountAsThresholdCamera is null");
                else if (value is not double) Debug.Log($"CONFIGURATION ERROR: lastPitchDistanceToCountAsThresholdCamera is not double is {value.GetType()}");
                else lastPitchDistanceToCountAsThresholdCamera = (double)value;
            else Debug.Log("CONFIGURATION ERROR: lastPitchDistanceToCountAsThresholdCamera not set");
        }
        { //kickWhenFullAfk
            if (baseConfigs.TryGetValue("kickWhenFullAfk", out object value))
                if (value is null) Debug.Log("CONFIGURATION ERROR: kickWhenFullAfk is null");
                else if (value is not bool) Debug.Log($"CONFIGURATION ERROR: kickWhenFullAfk is not boolean is {value.GetType()}");
                else kickWhenFullAfk = (bool)value;
            else Debug.Log("CONFIGURATION ERROR: kickWhenFullAfk not set");
        }
        { //kickMessageToPlayerKicked
            if (baseConfigs.TryGetValue("kickMessageToPlayerKicked", out object value))
                if (value is null) Debug.Log("CONFIGURATION ERROR: kickMessageToPlayerKicked is null");
                else if (value is not string) Debug.Log($"CONFIGURATION ERROR: kickMessageToPlayerKicked is not string is {value.GetType()}");
                else kickMessageToPlayerKicked = (string)value;
            else Debug.Log("CONFIGURATION ERROR: kickMessageToPlayerKicked not set");
        }
        { //enableExtendedLog
            if (baseConfigs.TryGetValue("enableExtendedLog", out object value))
                if (value is null) Debug.Log("CONFIGURATION ERROR: enableExtendedLog is null");
                else if (value is not bool) Debug.Log($"CONFIGURATION ERROR: enableExtendedLog is not boolean is {value.GetType()}");
                else enableExtendedLog = (bool)value;
            else Debug.Log("CONFIGURATION ERROR: enableExtendedLog not set");
        }
    }
    #endregion
}