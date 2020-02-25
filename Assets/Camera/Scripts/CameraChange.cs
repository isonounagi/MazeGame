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
    public GameObject zoomCamera_1st;

    [SerializeField]
    public GameObject zoomCamera_2nd;

    [SerializeField]
    public GameObject zoomCamera_3rd;

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
            mainCameraActivate += 1; //スペースを押すごとに加算、2で割り切れるときに2D画面
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

        if(mainCameraActivate == 0)　//ゲームが止まった時どの敵の追従カメラを有効にするか
        {
            mainCamera.SetActive(false);
            otherCamera.SetActive(false);

            if (EnemyMove.isEnemy1stTouched)
            {
                zoomCamera_1st.SetActive(true);
            }

            if (EnemyMove_2nd.isEnemy2ndTouched)
            {
                zoomCamera_2nd.SetActive(true);
            }

            if (EnemyMove_3rd.isEnemy3rdTouched)
            {
                zoomCamera_3rd.SetActive(true);
            }

        }
    }
}
