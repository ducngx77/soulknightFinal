using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Finish;

public class SelectCharacter : MonoBehaviour
{
    private int index;
    [SerializeField] GameObject[] listChars;
    [SerializeField] TextMeshProUGUI charName;
    public static GameObject selectedChar;
    void Start()
    {
        index = 0;
        SelectChar();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(index);
    }
    public void OnClickPrevBtn()
    {
        if (index > 0)
        {
            index --;
        } else if (index == 0)
        {
            index = listChars.Length - 1;
        }
        SelectChar();
    }

    public void OnClickNextBtn()
    {
        if (index < listChars.Length - 1)
        {
            index ++;
        } else if (index == listChars.Length - 1)
        {
            index = 0;
        }
        SelectChar();
    }

    private void SelectChar()
    {
        for (int i = 0; i < listChars.Length; i++)
        {
            if (i == index)
            {
                listChars[i].gameObject.SetActive(true);
                selectedChar = listChars[i];
                charName.text = selectedChar.name;   
            }
            else
            {
                listChars[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnClickPlayBtn()
    {
        SceneManager.LoadScene("Pet Selection");
        CharacterSelect c = new CharacterSelect(index, selectedChar.name);
        string json = JsonUtility.ToJson(c);
        File.WriteAllText(Application.dataPath + "/_Scripts/PlayerData/CharSelect.json", json);
    }
}
