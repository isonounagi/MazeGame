using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    //　メインカメラ
    [SerializeField]
    public GameObject mainCamera;
    //　切り替える他のカメラ
    [SerializeField]
    public GameObject otherCamera;

    [SerializeField]
    public GameObject zoomCamera;

    [SerializeField]
    private GameObject player;

    static public int mainCameraActivate = 1;

    

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //　Spaceキーを押したらカメラの切り替えをする
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainCamera.SetActive(!mainCamera.activeSelf);
            otherCamera.SetActive(!otherCamera.activeSelf);
            mainCameraActivate += 1;
        }

        if (otherCamera.activeSelf)
        {

            // マウスの右クリックを押している間
            if (Input.GetMouseButton(1))
                {
                    // マウスの移動量
                    float mouseInputX = Input.GetAxis("Mouse X");
                    float mouseInputY = Input.GetAxis("Mouse Y");
                    // targetの位置のY軸を中心に、回転（公転）する
                    otherCamera.transform.RotateAround(player.transform.position, Vector3.up, mouseInputX * Time.deltaTime * 200f);

                }
        }

        if(mainCameraActivate == 0)
        {
            mainCamera.SetActive(false);
            otherCamera.SetActive(false);
            zoomCamera.SetActive(true);
        }
    }
}
