using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;

public class SaveGame : MonoBehaviour 
{
    public JsonSaveGameSettings2 JsonDataScript = new JsonSaveGameSettings2();

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
        File.WriteAllText("Assets/SaveGameFile.json", json.ToString());
    }
    private void Load()
    {
        string dataPath = "Assets/SaveGameFile.json";
        string dataAsJson = File.ReadAllText(dataPath);
        JsonDataScript = JsonUtility.FromJson<JsonSaveGameSettings2>(dataAsJson);
    }

    void setSaveDaa()
    {

    }

    void getSaveData()
    {

    }
}
public class JsonSaveGameSettings2
{
    public int levelIndex;
    public int[] inventoryItemsIndex;
    public int[] attatchementsEquiped_Pistol;

}
