using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTouched : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        //まだアイテムがハンマーだけなのでハンマーのときの処理だけ書いている
        if (col.tag == "Player") //接触したとき
        {
            if(PlayerMove.isHummerUse == false) //ハンマーが一度使われていたら
            {
                Destroy(gameObject);
                PlayerMove.isHummerUse = true;　//ハンマーをもう一度使える
                UIDirector.hummerImage.enabled = true;　//UI画像も復活させる
            }
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
