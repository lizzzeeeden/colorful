using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;  //文本节点
    public Image faceImage; //立绘节点

    [Header("文本文件")]
    public TextAsset textFile;//文本文件
    private int index;
    public float textSpeed;//单字显示的延迟时间
    private float rTextSpeed;//实际文本速度（会变）
    private bool bLineTextFinished;//单句结束

    [Header("头像")]
    public Sprite face1;

    List<string> textList = new List<string>();

    //开始游戏时将文本先提取出来
    private void Awake()
    {
        GetTextFromFile(textFile);
    }
    //一些初始化工作
    private void OnEnable()
    {
        //player.gameObject.GetComponent<Player>().SetMobility(false);
        index = 0;
        bLineTextFinished = true;
        rTextSpeed = textSpeed;
        StartCoroutine(SetTextUI());
    }


    void Update()
    {
        //对话结束
        if (Input.GetKeyDown(KeyCode.Space) && index == textList.Count) {
            gameObject.SetActive(false);
            index = 0;
            //player.gameObject.GetComponent<Player>().SetMobility(true);
            return;
        }

        //对话继续
        if (Input.GetKeyDown(KeyCode.Space)) {
            //开始协程
            if (bLineTextFinished) {
                rTextSpeed = textSpeed;
                StartCoroutine(SetTextUI());
            } else {
                rTextSpeed = 0;//协程还未结束，在中间时更改协程时间
            }
        }
    }
    //将文本从文件提取出来
    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;
        var lineData = file.text.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);//此处lineData是字符串数组类型
        //windows换行是\r\n，后一个参数是去除空行（空行无法显示注意）

        foreach (var line in lineData) {
            textList.Add(line);//line提取数组每个字符串，加到lineData里去
        }
    }

    //展示文本函数
    IEnumerator SetTextUI()
    {
        bLineTextFinished = false;
        textLabel.text = "";

        //切换立绘
        switch (textList[index]) {
            case "A":
                faceImage.sprite = face1;
                index++;
                break;
            case "B":
                index++;
                break;
        }

        //逐字显示
        for (int i = 0; i < textList[index].Length; i++) {
            textLabel.text += textList[index][i];

            //协程停顿时间
            yield return new WaitForSeconds(rTextSpeed);
        }
        bLineTextFinished = true;
        index++;
    }
}
