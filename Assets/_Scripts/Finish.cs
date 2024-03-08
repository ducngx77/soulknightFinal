using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public CoinCount coinCounter;
    private bool levelComplete = false;
    private RoomFirstDungeonGenerator roomFirstDungeonGenerator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FindTarget.CheckFoundAllTarget("Enemy") == false && FindTarget.CheckFoundAllTarget("Boss") == false)
        {
            if (!levelComplete && collision.tag == "Player")
            {
                levelComplete = true;

                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<CoinCount>().UpLevel();

                coinCounter = FindObjectOfType<CoinCount>();
                int data = coinCounter.coinNumber;
                Coins c = new Coins(data);
                string json = JsonUtility.ToJson(c);
                File.WriteAllText(Application.dataPath + "/_Scripts/PlayerData/CoinConfig.json", json);
                int lv = player.GetComponent<CoinCount>().level;
                if (lv < 3)
                {
                    StartCoroutine(ReloadCurrentLevel());
                } else if (lv == 3)
                {
                    SceneManager.LoadScene("Boss");
                    
                } else if (lv > 3)
                {
                    lv = 0;
                    SceneManager.LoadScene("Win");
                    Debug.Log("Won the game");
                }
                
            }
        }
    }

    private IEnumerator ReloadCurrentLevel()
    {
        yield return new WaitForSeconds(1f);

        // Tìm và kích hoạt RoomFirstDungeonGenerator trong cảnh hiện tại (Level 1)
        if (roomFirstDungeonGenerator == null)
        {
            roomFirstDungeonGenerator = FindObjectOfType<RoomFirstDungeonGenerator>();
        }

        if (roomFirstDungeonGenerator != null)
        {
            // Reset cảnh hiện tại (Level 1) bằng cách tạo lại bản đồ
            roomFirstDungeonGenerator.CreateRooms();
        }
        else
        {
            Debug.LogWarning("Không tìm thấy RoomFirstDungeonGenerator trong cảnh hiện tại (Level 1).");
        }

        levelComplete = false; // Đặt lại biến levelComplete để cho phép kích hoạt lại khi qua màn tiếp theo
        Destroy(gameObject);
    }
    [System.Serializable]
    public class Coins
    {
        public int coinValue;

        public Coins(int coinValue)
        {
            this.coinValue = coinValue;
        }
    }

    [System.Serializable]
    public class CharacterSelect
    {
        public int index;
        public string name;
        public CharacterSelect(int index, string name)
        {
            this.index = index;
            this.name = name;
        }
    }
    [System.Serializable]
    public class PetSelect
    {
        public int index;
        
        public string name;
        public int cost;
        public bool bought;
        public bool selected;

        public PetSelect(int index, string name, int cost, bool bought, bool selected)
        {
            this.index = index;
            this.name = name;
            this.cost = cost;
            this.bought = bought;
            this.selected = selected;
        }
    }
}
