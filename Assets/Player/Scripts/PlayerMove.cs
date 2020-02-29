using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{   
    private CharacterController characterController;
    private Animator animator;
    private Vector3 velocity;

    private GameObject player;
    public static Vector3 playerPos; //プレイヤーの位置を他から参照できるようにする（敵の追跡などに使う）
    
    [SerializeField]
    private float moveSpeed;

    public static bool isAnyKeyEnabled;　//キー操作を有効にするかどうかのフラグ
    public static bool isHummerUse; //ハンマーを使ったかどうか

    Ray ray; //自機の前から光線を出す
    RaycastHit hit;
    int distance;


    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("knight");
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        isAnyKeyEnabled = true;
        isHummerUse = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        playerPos = player.transform.position;


        //Rayの作成　　　　　　　
        ray = new Ray(playerPos, player.transform.TransformDirection(Vector3.forward));

        //Rayの飛ばせる距離
        distance = 1;


        //Rayの可視化 
        Debug.DrawRay(playerPos, player.transform.TransformDirection(Vector3.forward) * distance, Color.red);

        if (isAnyKeyEnabled) //キー操作が有効なとき
        {
            if (CameraChange.mainCameraActivate % 2 == 0) //2で割り切れるときに2D画面へ
            {
                velocity = new Vector3(0, 0, 0);

                if (characterController.isGrounded)
                {
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        characterController.Move(this.gameObject.transform.forward * moveSpeed * Time.deltaTime);//前方にMoveSpeed * 時間経過分動かす
                    }
                    /*if (Input.GetKey(KeyCode.S))
                    {
                        characterController.Move(this.gameObject.transform.forward * -1 * moveSpeed * Time.deltaTime);//後方にMoveSpeed * 時間経過分動かす
                    }*/
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        transform.Rotate(new Vector3(0, -5, 0));//左回転する        
                    }
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        transform.Rotate(new Vector3(0, 5, 0));//右回転する        
                    }

                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        animator.SetFloat("Speed", 1f);
                    }
                    else
                    {
                        animator.SetFloat("Speed", 0f);
                    }
                }
            }
            else
            {
                if (characterController.isGrounded)
                {
                    velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

                    if (velocity.magnitude > 0.1f)
                    {
                        animator.SetFloat("Speed", velocity.magnitude);
                        transform.LookAt(transform.position + velocity);
                    }
                    else
                    {
                        animator.SetFloat("Speed", 0f);
                    }

                }
                
            }

            if (isHummerUse)
            {
                if (Input.GetKeyDown(KeyCode.C)) //Cを押すと目の前の壁が消える
                {
                    if (Physics.Raycast(ray, out hit, distance))
                    {
                        //Rayが当たったオブジェクトのtagがPlayerだったら
                        if (hit.collider.tag == "Wall")
                        {
                            Destroy(hit.transform.gameObject);　//ハンマーの効果。目の前の壁を消す。
                            Destroy(hit.transform.gameObject);
                            Destroy(hit.transform.gameObject);
                            isHummerUse = false; //使えるのは一回だけ

                            UIDirector.hummerImage.enabled = false;　//UI画像を非表示に
                        }

                    }
                }
            }

        }

        if (CameraChange.mainCameraActivate == 0)　//0のときに停止するように
        {
            moveSpeed = 0;
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * moveSpeed * Time.deltaTime);
    }
    
}
