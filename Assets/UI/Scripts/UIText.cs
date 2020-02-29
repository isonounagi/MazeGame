using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player") //ゴールにたどり着いたとき
        {
            UIDirector.goal.enabled = true;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        UIDirector.goal.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
