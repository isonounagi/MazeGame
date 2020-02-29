using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDirector : MonoBehaviour
{
    private Canvas canvas;
    static public Image hummerImage; //UIのハンマーの画像を格納

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();

        foreach (Transform child in canvas.transform)
        {
            if (child.name == "Hummer_Image")
            {
                hummerImage = child.gameObject.GetComponent<Image>();　//UI画像を表示
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
