using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SceneColorManager : MonoBehaviour
{
    Color32 color;
    Color32 red = new Color32(225, 130, 130, 255);
    Color32 purple=new Color32(230, 182, 255, 255);


    public void ChangeSpColor(char c)
    {
        if (c == 'r') {
            color = red;
        }else if (c == 'p') {
            color = purple;
        }

        SpriteRenderer[] sp = transform.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sp.Length; i++) {
            sp[i].color = new Color32(225, 130, 130, 255);
        }
    }

    public void ChangeTmColor(char c)
    {
        if (c == 'r') {
            color = red;
        } else if (c == 'p') {
            color = purple;
        }

        Tilemap[] tm = transform.GetComponentsInChildren<Tilemap>();
        for (int i = 0; i < tm.Length; i++) {
            tm[i].color = new Color32(225, 130, 130, 255);
        }
    }

    public void ChangeImColor(char c)
    {
        if (c == 'r') {
            color = red;
        } else if (c == 'p') {
            color = purple;
        }

        Image[] im = transform.GetComponentsInChildren<Image>();
        for (int i = 0; i < im.Length; i++) {
            im[i].color = new Color32(225, 130, 130, 255);
        }
        Debug.Log(im.Length);

    }
}
