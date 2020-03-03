using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    private GameObject[] AutoWallPosTag;
    private int getLength;
    
    [SerializeField]
    public GameObject hummer;

    private void SetItem(GameObject itemPrefab)
    {
            for (int p = 1; p <= getLength - 1; p++)
            {
                if (AutoWallPosTag[p].tag == "Path")
                {
                    int r = Random.Range(1, 21); //アイテムが置かれる確率
                    if (r == 1)
                    {
                        Instantiate(itemPrefab,AutoWallPosTag[p].transform);
                    }

                }
            }
    }

    // Start is called before the first frame update
    void Start()
    {
        AutoWallPosTag = GameObject.FindGameObjectsWithTag("Path"); //通り道を格納

        getLength = AutoWallPosTag.Length;　//配列の長さを取得

        Debug.Log(getLength);

        SetItem(hummer);　//アイテム（ハンマーの設置）
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
