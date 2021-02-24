using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    
    public static GameControl control;
   
    public int SceneNumber;
    public GameObject Player;
    PlayerStats PlayerST;
    public float playerHealth;
    public float playerStamina;
    public Vector3 lastCheckpoint;
    public int XpRequired;
    public int xpGainedInLevel;
    public int totalXp;
    Text Message;
    public int PlayerLevel;
    public int playerGold;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

        
        PlayerST = Player.GetComponent<PlayerStats>();
        //StartCoroutine(FindText());
    }

    // Update is called once per frame
    void Update()
    {


        //playerHealth = Player.GetComponent<PlayerStats>().health;
        //playerStamina = PlayerController.stamina;
        //xpGainedInLevel = Player.GetComponent<PlayerStats>().experience;
        //totalXp = Player.GetComponent<PlayerStats>().totalExperience;
        //XpRequired = Player.GetComponent<PlayerStats>().xpLimit;
        //playerGold = Player.GetComponent<PlayerStats>().gold;
        //PlayerLevel = Player.GetComponent<PlayerStats>().levelUpCounter;

        if (Input.GetKeyDown(KeyCode.F1))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Load();
        }
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerSaveData.dat");
        PlayerData data = new PlayerData();
        SceneNumber = SceneManager.GetActiveScene().buildIndex;
        data.playerHealth = playerHealth;
        data.XpRequired = XpRequired;
        data.playerStamina = playerStamina;
        data.xpGainedInLevel = xpGainedInLevel;
        data.totalXp = totalXp;
        data.playerGold = playerGold;
        data.PlayerLevel = PlayerLevel;
        data.SceneToLoad = SceneNumber;
        bf.Serialize(file, data);
        file.Close();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerSaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerSaveData.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            playerHealth = data.playerHealth;
            XpRequired = data.XpRequired;
            playerStamina = data.playerStamina;
            xpGainedInLevel = data.xpGainedInLevel;
            totalXp = data.totalXp;
            playerGold = data.playerGold;
            PlayerLevel = data.PlayerLevel;
            SceneNumber = data.SceneToLoad;
            StartCoroutine(LoadNewScene());
        }
    }
    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneNumber);

        while (!async.isDone)
        {
            yield return null;
        }
        if (async.isDone)
        {
          
            SceneManager.LoadScene(SceneNumber);
            //PlayerST.LoadGame();
        }
    }

    //IEnumerator FindText()
    //{
    //    yield return new WaitForSeconds(3);
    //    Message = GameObject.Find("GameControl/Player/InGameCanvas/Message").GetComponent<Text>();
    //    if(Message == null)
    //    {
    //        yield return null;
    //    }
    //    else
    //    {
    //        Message = GameObject.Find("GameControl/Player/InGameCanvas/Message").GetComponent<Text>();
    //    }
    //}
}
[Serializable]

class PlayerData
{
    public float playerHealth;
    public float playerStamina;

    public int XpRequired;
    public int xpGainedInLevel;
    public int totalXp;

    public int PlayerLevel;
    public int playerGold;
    public int SceneToLoad;
}
