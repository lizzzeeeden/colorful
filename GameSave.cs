using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//数据的输入输出
using System.Runtime.Serialization.Formatters.Binary;//转为二进制（Jing的记忆数据x）
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSave : MonoBehaviour
{
    public PlayerData playerData;
    public PlayerData saveTimeData;
    public InventorySO objectBag;
    public InventorySO bulletBag;

    public GameObject loadUI;
    public GameObject saveUI;

    public void SaveGame(int n)
    {
        //存储玩家物品数据到playerData里
        playerData.SaveItems(objectBag.GetList(), bulletBag.GetList());
        saveTimeData.SaveSaveTime(System.DateTime.Now.ToString(), n);

        //Debug.Log(Application.persistentDataPath);
        //创建存档文件夹
        if (!Directory.Exists(Application.persistentDataPath + "/SaveData")) {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData");
        }

        BinaryFormatter fomatter1 = new BinaryFormatter();//将数据加密为二进制
        
        FileStream file1;
        var json1 = JsonUtility.ToJson(playerData);
        //根据n决定写入哪个存档
        switch (n) {
            case 0:
                file1 = File.Create(Application.persistentDataPath + "/SaveData/PlayerData0.txt");
                fomatter1.Serialize(file1, json1);
                file1.Close();
                break;
            case 1:
                file1 = File.Create(Application.persistentDataPath + "/SaveData/PlayerData1.txt");
                fomatter1.Serialize(file1, json1);
                file1.Close();
                break;
            case 2:
                file1 = File.Create(Application.persistentDataPath + "/SaveData/PlayerData2.txt");
                fomatter1.Serialize(file1, json1);
                file1.Close();
                break;
            case 3:
                file1 = File.Create(Application.persistentDataPath + "/SaveData/PlayerData3.txt");
                fomatter1.Serialize(file1, json1);
                file1.Close();
                break;
            case 4:
                file1 = File.Create(Application.persistentDataPath + "/SaveData/PlayerData4.txt");
                fomatter1.Serialize(file1, json1);
                file1.Close();
                break;
            case 5:
                file1 = File.Create(Application.persistentDataPath + "/SaveData/PlayerData5.txt");
                fomatter1.Serialize(file1, json1);
                file1.Close();
                break;
            case 6:
                file1 = File.Create(Application.persistentDataPath + "/SaveData/PlayerData6.txt");
                fomatter1.Serialize(file1, json1);
                file1.Close();
                break;
        }

        //保存存档时间
        BinaryFormatter fomatter2 = new BinaryFormatter();//将数据加密为二进制
        FileStream file2 = File.Create(Application.persistentDataPath + "/SaveData/DateData.txt");
        var json2 = JsonUtility.ToJson(saveTimeData);
        fomatter2.Serialize(file2, json2);
        file2.Close();
        SceneManager.LoadScene(0);

    }

    //抄别人的代码请抄齐全点
    public void LoadGame(int n)
    {
        BinaryFormatter fomatter = new BinaryFormatter();
        FileStream file;
        switch (n) {
            case 0:
                if (File.Exists(Application.persistentDataPath + "/SaveData/PlayerData0.txt")) {
                    file = File.Open(Application.persistentDataPath + "/SaveData/PlayerData0.txt", FileMode.Open);
                    JsonUtility.FromJsonOverwrite((string)fomatter.Deserialize(file), playerData);
                    file.Close();
                } else {
                    return;
                }
                break;
            case 1:
                if (File.Exists(Application.persistentDataPath + "/SaveData/PlayerData1.txt")) {
                    file = File.Open(Application.persistentDataPath + "/SaveData/PlayerData1.txt", FileMode.Open);
                    JsonUtility.FromJsonOverwrite((string)fomatter.Deserialize(file), playerData);
                    file.Close();
                } else {
                    return;
                }
                break;
            case 2:
                if (File.Exists(Application.persistentDataPath + "/SaveData/PlayerData2.txt")) {
                    file = File.Open(Application.persistentDataPath + "/SaveData/PlayerData2.txt", FileMode.Open);
                    JsonUtility.FromJsonOverwrite((string)fomatter.Deserialize(file), playerData);
                    file.Close();
                } else {
                    return;
                }
                break;
            case 3:
                if (File.Exists(Application.persistentDataPath + "/SaveData/PlayerData3.txt")) {
                    file = File.Open(Application.persistentDataPath + "/SaveData/PlayerData3.txt", FileMode.Open);
                    JsonUtility.FromJsonOverwrite((string)fomatter.Deserialize(file), playerData);
                    file.Close();
                } else {
                    return;
                }
                break;
            case 4:
                if (File.Exists(Application.persistentDataPath + "/SaveData/PlayerData4.txt")) {
                    file = File.Open(Application.persistentDataPath + "/SaveData/PlayerData4.txt", FileMode.Open);
                    JsonUtility.FromJsonOverwrite((string)fomatter.Deserialize(file), playerData);
                    file.Close();
                } else {
                    return;
                }
                break;
            case 5:
                if (File.Exists(Application.persistentDataPath + "/SaveData/PlayerData5.txt")) {
                    file = File.Open(Application.persistentDataPath + "/SaveData/PlayerData5.txt", FileMode.Open);
                    JsonUtility.FromJsonOverwrite((string)fomatter.Deserialize(file), playerData);
                    file.Close();
                } else {
                    return;
                }
                break;
            case 6:
                if (File.Exists(Application.persistentDataPath + "/SaveData/PlayerData6.txt")) {
                    file = File.Open(Application.persistentDataPath + "/SaveData/PlayerData6.txt", FileMode.Open);
                    JsonUtility.FromJsonOverwrite((string)fomatter.Deserialize(file), playerData);
                    file.Close();
                } else {
                    return;
                }
                break;
        }

        //把数据加载给objectBag和bulletBag
        objectBag.SetList(playerData.GetObjects());
        bulletBag.SetList(playerData.GetBullets());

        //场景跳转
        SceneManager.LoadScene(1);
    }

    public void OpenStartLoad()
    {
        //这个是为了写入日期还蛮傻的，用于开始界面的load界面（每次开启游戏时需要）
        //日期也是存储在palyerdata里，所以每次需要
        if(!File.Exists(Application.persistentDataPath + "/SaveData/DateData.txt")) {
            return;
        }
        
        BinaryFormatter fomatter = new BinaryFormatter();
        FileStream file;
        file = File.Open(Application.persistentDataPath + "/SaveData/DateData.txt", FileMode.Open);
        JsonUtility.FromJsonOverwrite((string)fomatter.Deserialize(file), saveTimeData);
        file.Close();

        OpenLoadUI();
    }

    public void OpenLoadUI()
    {
        Text[] timeText = loadUI.transform.GetComponentsInChildren<Text>();
        string[] saveTime = saveTimeData.GetSaveTime();

        for (int i = 0; i < saveTime.Length; i++) {
            if (saveTime[i] == null) {
                continue;
            } else if (i == 0) {
                timeText[i].text = "临时存档"+ "\n" + saveTime[i].ToString();
            } else {
                timeText[i].text = "存档" + i.ToString() + "\n" + saveTime[i].ToString();
            }
        }

        loadUI.SetActive(true);
    }
    public void OpenSaveUI()
    {
        Text[] timeText = saveUI.transform.GetComponentsInChildren<Text>();
        string[] saveTime = saveTimeData.GetSaveTime();

        for (int i = 0; i < saveTime.Length; i++) {
            if (saveTime[i] == null) {
                continue;
            } else if (i == 0) {
                timeText[i].text = "临时存档" + "\n" + saveTime[i].ToString();
            } else {
                timeText[i].text = "存档 " + i.ToString() + "\n" + saveTime[i].ToString();
            }
        }

        saveUI.SetActive(true);
    }


    public void QuitGame()
    {
        SaveGame(0);
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
