using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    List<Transform> containerPanels;


    public Character[] characters;
    public Image characterImage;
    public Text characterLore;
    int currentCharacter = 0;

    void Awake()
    {
        containerPanels = new List<Transform>();
        GameObject canvas = GameObject.Find("Canvas");

        for (int i = 0; i < canvas.transform.childCount; i++)
            containerPanels.Add(canvas.transform.GetChild(i));
    }

    void Start()
    {
        ShowContainerPanel("MainPanel");
        ChangeCharacterButton(0);
    }

    public void ShowContainerPanel(string name)
    {
        foreach (Transform t in containerPanels)
        {
            if(t.gameObject.name == name)
            {
                t.gameObject.SetActive(true);
            }
            else
            {
                t.gameObject.SetActive(false);
            }
        }

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeCharacterButton(int i)
    {
        currentCharacter += i;
        if (currentCharacter < 0)
            currentCharacter = characters.Length - 1;
        else if(currentCharacter > characters.Length-1)
            currentCharacter = 0;
        characterImage.sprite = characters[currentCharacter].sprite;
        characterLore.text = characters[currentCharacter].lore.Replace("\\n","\n");
        PlayerPrefs.SetInt("SelectedCharacter",currentCharacter);
    }

    public void StartNewButton()
    {
        PlayerPrefs.SetInt("NewGame", 1);
        SceneManager.LoadScene("Level1");
    }

    
}
