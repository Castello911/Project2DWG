using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingItem : MonoBehaviour
{
    public string[] settingTexts;
    public int currentSetting = 0;

    Text text;

    void Awake()
    {
        text = transform.GetChild(0).GetComponentInChildren<Text>();
    }
    void Update()
    {
        currentSetting = Mathf.Clamp(currentSetting, 0, settingTexts.Length - 1);
        if (settingTexts.Length > 0)
        {
            text.text = settingTexts[currentSetting];
        }
    }

    public void Increase()
    {
        currentSetting++;
    }
    
    public void Decrease()
    {
        currentSetting--;
    }

    
}
