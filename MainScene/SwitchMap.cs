#pragma warning disable CS0649
//关闭Idx初始化都为默认值的警告

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchMap : MonoBehaviour
{
    [SerializeField]
    private int thisMapIdx;
    [SerializeField]
    private int nextMapIdx;
    [SerializeField]
    private int nextTriIdx;

    private CinemachineConfiner camaraCF;
    private GameObject[] maps;
    private GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camaraCF = GameObject.FindGameObjectWithTag("Camara").GetComponent<CinemachineConfiner>();

        Transform mapParent = transform.parent.parent.parent;
        maps = new GameObject[mapParent.childCount];
        for (int i = 0; i < mapParent.childCount; i++) {
            maps[i] = mapParent.GetChild(i).gameObject;
        }

        if (gameObject.activeSelf) {
            camaraCF.m_BoundingShape2D = transform.parent.parent.Find("Tilemap(Basal)").GetComponent<PolygonCollider2D>();
            player.transform.position = transform.parent.parent.Find("Exit").GetChild(0).position;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            maps[thisMapIdx].SetActive(false);
            maps[nextMapIdx].SetActive(true);
            camaraCF.m_BoundingShape2D = maps[nextMapIdx].transform.Find("Tilemap(Basal)").GetComponent<PolygonCollider2D>();

            player.transform.position = maps[nextMapIdx].transform.Find("Exit").GetChild(nextTriIdx).position;
        }
    }

}
