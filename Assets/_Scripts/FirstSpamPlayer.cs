using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Finish;
using static SelectPet;

public class FirstSpamPlayer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> playerPrefab;
    [SerializeField]
    private List<GameObject> petPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject pet = GameObject.FindGameObjectWithTag("Pet");
        if (player == null)
        {
            Instantiate(GetCharSelected(), new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            player.transform.position = new Vector3(0, 0, 0);
        }
        if (pet == null && GetPetSelected() != null)
        {
            Instantiate(GetPetSelected(), new Vector3(0, 0, 0), Quaternion.identity);
        }
        else if (pet != null) 
        {
            pet.transform.position = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject GetCharSelected()
    {
        string json = File.ReadAllText(Application.dataPath + "/_Scripts/PlayerData/CharSelect.json");
        CharacterSelect data = JsonUtility.FromJson<CharacterSelect>(json);
        int index = (int)data.index;
        string name  = data.name;
        return playerPrefab[index];
    }

    GameObject GetPetSelected()
    {
        string jsonRead2 = File.ReadAllText(Application.dataPath + "/_Scripts/PlayerData/PetSelect.json");
        PetData petData = JsonUtility.FromJson<PetData>(jsonRead2);
        foreach (var pet in petData.pet)
        {
            if (pet.selected == true)
            {
                 return petPrefab[pet.index];
            }
        }
        return null;
    }
}
