using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    public Slider musicSlider;
    public Slider effectsSlider;

    private string jsonSavePath;
    public SettingsData _settingsData;

    void Awake()
    {
        jsonSavePath = Application.persistentDataPath + "/Settings.json";
        Load();
    }

    public void Save()
    {
        //Tworzenie pliku i jego lokalizacji
        FileStream File1 = new FileStream(jsonSavePath, FileMode.OpenOrCreate);

        //Zapisywanie danych
        string jsonData = JsonUtility.ToJson(_settingsData, true);

        _settingsData.musicVolume = musicSlider.value;
        _settingsData.effectsVolume = effectsSlider.value;

        File1.Close();
        File.WriteAllText(jsonSavePath, jsonData);
    }

    public void Load()
    {
        if (Directory.Exists(jsonSavePath))
        {
            string json = ReadFromFile();
            JsonUtility.FromJsonOverwrite(json, _settingsData);

            //£adowanie danych
            musicSlider.value = _settingsData.musicVolume;
            effectsSlider.value = _settingsData.effectsVolume;
        }
    }

    private string ReadFromFile()
    {
        using (StreamReader Reader = new StreamReader(jsonSavePath))
        {
            string json = Reader.ReadToEnd();
            return json;
        }
    }
}