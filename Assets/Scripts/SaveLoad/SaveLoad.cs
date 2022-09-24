using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    public Slider musicSlider;
    public Slider effectsSlider;

    private string jsonSavePath;
    public SettingsData _settingsData;
    public GameGraphicsController _graphicsController;

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

        //Audio
        _settingsData.musicVolume = musicSlider.value;
        _settingsData.effectsVolume = effectsSlider.value;

        //Graphics
        _settingsData.fullScreen = _graphicsController.fullscreen;
        _settingsData.vSync = _graphicsController.vsync;
        _settingsData.resolutionValue = _graphicsController.resolutionDropdown.value;

        File1.Close();
        File.WriteAllText(jsonSavePath, jsonData);
    }

    public void Load()
    {
        if (File.Exists(jsonSavePath))
        {
            string json = ReadFromFile();
            JsonUtility.FromJsonOverwrite(json, _settingsData);

            //£adowanie danych
            //Audio
            musicSlider.value = _settingsData.musicVolume;
            effectsSlider.value = _settingsData.effectsVolume;

            //Graphics
            _graphicsController.fullscreen = _settingsData.fullScreen;
            _graphicsController.vsync = _settingsData.vSync;
            _graphicsController.resolutionDropdown.value = _settingsData.resolutionValue;
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