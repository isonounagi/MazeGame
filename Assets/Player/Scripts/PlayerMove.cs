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

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("knight");
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        playerPos = player.transform.position;

        if (CameraChange.mainCameraActivate % 2 == 0)
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
        else
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
        
        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * moveSpeed * Time.deltaTime);
    }
}
