using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIText : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player") //ゴールにたどり着いたとき
        {
            UIDirector.goal.enabled = true;
        }
    }

    private void SetText() //テキストのUIを表示する
    {
        UIDirector.goal.enabled = true;
    }

    private void SetEnd()
    {
        UIDirector.goal.text = "GAME OVER";
        UIDirector.goal.enabled = true;
    }

    private void ReStart()
    {
        CameraChange.mainCameraActivate = 1;

        SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        UIDirector.goal.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(CameraChange.mainCameraActivate == 0)　//敵につかまったとき、ゲームオーバーの文字を表示後、リスタート
        {
            Invoke("SetEnd", 3.0f);
            Invoke("ReStart", 4.5f);
        }
    }
}
