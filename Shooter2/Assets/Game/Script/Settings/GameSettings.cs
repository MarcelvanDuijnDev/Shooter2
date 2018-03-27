using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;

public class GameSettings : MonoBehaviour 
{
    public JsonSaveGameSettings JsonDataScript = new JsonSaveGameSettings();

    //Local Settings
    private bool local_FullScreen;
    private int local_Resolution;

    void Start () 
    {
        Load();
    }

    //Save / Load
    private void Save()
    {
        string json = JsonUtility.ToJson(JsonDataScript);
        File.WriteAllText("Assets/GameSettings.json", json.ToString());
    }
    private void Load()
    {
        string dataPath = "Assets/GameSettings.json";
        string dataAsJson = File.ReadAllText(dataPath);
        JsonDataScript = JsonUtility.FromJson<JsonSaveGameSettings>(dataAsJson);
        setLocalSettings();
        setSettings();
    }

    public void setSettings()
    {
        if (JsonDataScript.resolution == 1)
        {
            Screen.SetResolution(1920, 1080, JsonDataScript.fullScreen);
        }
    }

    public void setLocalSettings()
    {
        local_FullScreen = JsonDataScript.fullScreen;
        local_Resolution = JsonDataScript.resolution;
    }

    public void ApplyNewSettings()
    {
        JsonDataScript.fullScreen = local_FullScreen;
        JsonDataScript.resolution = local_Resolution;
        setSettings();
    }
}
public class JsonSaveGameSettings
{
    public bool fullScreen;
    public int resolution;
}
