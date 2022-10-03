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
    public GameUIController _gameUI;

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
        _settingsData.fullScreen = _graphicsController.fullScreen;
        _settingsData.vSync = _graphicsController.vSync;
        _settingsData.resolutionValue = _graphicsController.resolutionDropdown.value;

        //GUI
        _settingsData.showingInfo = _gameUI.menuInfoToggle.isOn;

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
            _graphicsController.fullScreen = _settingsData.fullScreen;
            _graphicsController.vSync = _settingsData.vSync;
            _graphicsController.resolutionValue = _settingsData.resolutionValue;
            _graphicsController.UpdateGraphics();

            //GUI
            _gameUI.menuInfoToggle.isOn = _settingsData.showingInfo;
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