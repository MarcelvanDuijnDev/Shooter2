using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;

public class UserSettings : MonoBehaviour 
{
    public JsonSaveUserSettings JsonDataScript = new JsonSaveUserSettings();

    //Local Settings
    private float local_MouseSensivity;

    void Start () 
    {
        Load();
    }

    //Save / Load
    private void Save()
    {
        string json = JsonUtility.ToJson(JsonDataScript);
        File.WriteAllText("Assets/UserSettings.json", json.ToString());
    }
    private void Load()
    {
        string dataPath = "Assets/UserSettings.json";
        string dataAsJson = File.ReadAllText(dataPath);
        JsonDataScript = JsonUtility.FromJson<JsonSaveUserSettings>(dataAsJson);
        setLocalSettings();
        setSettings();
    }

    public void setSettings()
    {
        GameObject playerObj = GameObject.Find("Player");
        playerObj.GetComponent<CharacterController_Script>().cameraSensitivity = JsonDataScript.mouseSensivity;
    }

    public void setLocalSettings()
    {
        local_MouseSensivity = JsonDataScript.mouseSensivity;
    }

    public void ApplyNewSettings()
    {
        JsonDataScript.mouseSensivity = local_MouseSensivity;
        setSettings();
    }
}
public class JsonSaveUserSettings
{
    public float mouseSensivity;
}
