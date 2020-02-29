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
                    int r = Random.Range(1, 11);
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
        AutoWallPosTag = GameObject.FindGameObjectsWithTag("Path");

        getLength = AutoWallPosTag.Length;

        Debug.Log(getLength);

        SetItem(hummer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
