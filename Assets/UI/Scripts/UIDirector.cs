using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDirector : MonoBehaviour
{
    private Canvas canvas;
    static public Image hummerImage; //UIのハンマーの画像を格納
    static public Text goal; //UIのゴールのテキストを格納

    // Start is called before the first frame update
    void Awake() //Startより前に取得する
    {
        canvas = GetComponent<Canvas>();

        foreach (Transform child in canvas.transform) //Canvasの中身を順に調べ名前に合致したものを格納する
        {
            if (child.name == "Hummer_Image")
            {
                hummerImage = child.gameObject.GetComponent<Image>();　//UI画像を表示
            }
            else if(child.name == "Goal")
            {
                goal = child.gameObject.GetComponent<Text>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
