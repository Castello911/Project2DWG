using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    MainMenuManager mainMenuManager;

    public SettingItem musicLevelItem,fxLevelItem;
    public int musicLevel,fxLevel;

    void Awake()
    {
        mainMenuManager = GetComponent<MainMenuManager>();
    }

    void Start()
    {
        WriteOnUI();
    }

    public void WriteOnUI()
    {
        musicLevelItem.currentSetting = PlayerPrefs.GetInt("MusicLevel");
        fxLevelItem.currentSetting = PlayerPrefs.GetInt("FXLevel");
    }

    public void ApplyButton()
    {
        PlayerPrefs.SetInt("MusicLevel",musicLevelItem.currentSetting);
        PlayerPrefs.SetInt("FXLevel", fxLevelItem.currentSetting);
        DiscardButton();
    }

    public void DiscardButton()
    {
        GetComponent<AudioManager>().SetAll();
        mainMenuManager.ShowContainerPanel("MainPanel");
        WriteOnUI();
    }

    public void AdjustMusicLevel(int val)
    {
        if (val < 0)
        {
            musicLevelItem.Decrease();
        }else if (val > 0)
        {
            musicLevelItem.Increase();
        }
    }

    public void AdjustFXLevel(int val)
    {
        if (val < 0)
        {
            fxLevelItem.Decrease();
        }
        else if (val > 0)
        {
            fxLevelItem.Increase();
        }
    }
}
