using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Finish;

public class SelectPet : MonoBehaviour
{
    private int index;
    [SerializeField] GameObject[] listPets;
    [SerializeField] GameObject[] buttons;
    [SerializeField] TextMeshProUGUI petName;
    public static GameObject selectedPet;
    void Start()
    {
        index = 0;
        SelectPetz();
        ChangeButton();
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
            index--;
        }
        else if (index == 0)
        {
            index = listPets.Length - 1;
        }
        SelectPetz();
        ChangeButton();
    }

    public void OnClickNextBtn()
    {
        if (index < listPets.Length - 1)
        {
            index++;
        }
        else if (index == listPets.Length - 1)
        {
            index = 0;
        }
        SelectPetz();
        ChangeButton();
    }

    private void SelectPetz()
    {
        for (int i = 0; i < listPets.Length; i++)
        {
            if (i == index)
            {
                listPets[i].gameObject.SetActive(true);
                selectedPet = listPets[i];
                petName.text = selectedPet.name;
            }
            else
            {
                listPets[i].gameObject.SetActive(false);
            }
        }
    }
    private void ChangeButton()
    {
        Debug.Log(index);
        string jsonRead = File.ReadAllText(Application.dataPath + "/_Scripts/PlayerData/PetSelect.json");
        PetData petData = JsonUtility.FromJson<PetData>(jsonRead);
        foreach (var pet in petData.pet)
        {
            if (pet.index == index)
            {
                Debug.Log( pet.index + "Bought" + pet.bought);
                if (pet.bought == false)
                {
                    buttons[0].SetActive(false);
                    buttons[1].SetActive(true);
                }
                else if (pet.bought == true)
                {
                    buttons[0].SetActive(true);
                    buttons[1].SetActive(false);
                }
            }
        }
    }

    public void OnClickBuyBtn()
    {

        string jsonRead1 = File.ReadAllText(Application.dataPath + "/_Scripts/PlayerData/CoinConfig.json");
        Coins data = JsonUtility.FromJson<Coins>(jsonRead1);
        Debug.Log(data.coinValue);

        string jsonRead2 = File.ReadAllText(Application.dataPath + "/_Scripts/PlayerData/PetSelect.json");
        PetData petData = JsonUtility.FromJson<PetData>(jsonRead2);

        foreach (var pet in petData.pet)
        {
            if (pet.index == index)
            {
                if (data.coinValue >= pet.cost)
                {
                    pet.bought = true;
                    data.coinValue -= pet.cost;
                } else
                {
                    Debug.Log("Khong du tien");
                }
            }
        }
        string updatedPetJsonData = JsonUtility.ToJson(petData);
        File.WriteAllText(Application.dataPath + "/_Scripts/PlayerData/PetSelect.json", updatedPetJsonData);
        string updatedCoinJsonData = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + "/_Scripts/PlayerData/CoinConfig.json", updatedCoinJsonData);
        buttons[0].SetActive(true);
        buttons[1].SetActive(false);
    }

    public void OnClickPlayBtn()
    {
        string jsonRead = File.ReadAllText(Application.dataPath + "/_Scripts/PlayerData/PetSelect.json");
        PetData petData = JsonUtility.FromJson<PetData>(jsonRead);

        foreach (var pet in petData.pet)
        {
            if (pet.index == this.index)
            {
                pet.selected = true;
            }
            else
            {
                pet.selected = false;
            }
        }
        string updatedJsonData = JsonUtility.ToJson(petData);
        File.WriteAllText(Application.dataPath + "/_Scripts/PlayerData/PetSelect.json", updatedJsonData);
        SceneManager.LoadScene("Level 1");
    }

    [System.Serializable]
    public class PetData
    {
        public List<PetSelect> pet;
    }
}
