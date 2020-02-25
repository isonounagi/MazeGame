using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private GameObject enemy;
    private Vector3 enemyPos;
    private CharacterController enemyController;
    private Rigidbody rigidbody;

    private Vector3 dir; //自機と敵との距離
    private GameObject mainCamera;
    private GameObject otherCamera;
    private GameObject zoomCamera;

    private Animator enemyAnimator;

    private NavMeshAgent enemyNavMesh;

    [SerializeField]
    private float enemyMoveSpeed;

    static public bool isEnemy1stTouched = false;　//敵が自機に接触したときにtrue

    private void Start()
    {
        enemyController = GetComponent<CharacterController>();
        enemyNavMesh = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        mainCamera = GameObject.Find("Main Camera");
        otherCamera = GameObject.Find("Camera");
        zoomCamera = GameObject.Find("ZoomCamera");
    }

    private void Update()
    {

        enemy = GameObject.FindGameObjectWithTag("Enemy1st");
        enemyPos = enemy.transform.position;

        if (PlayerMove.playerPos != null)
        {
            enemyNavMesh.destination = PlayerMove.playerPos; //自機を追跡

            dir = PlayerMove.playerPos - enemyPos;
            float d = dir.magnitude;　//敵と自機の間の距離
            
            enemyAnimator.SetFloat("Speed", enemyMoveSpeed);

            if (d <= 1.3f)　//敵と自機の間の距離が1.3f以下になったとき
            {
                CameraChange.mainCameraActivate = 0;

                enemyAnimator.SetTrigger("attack1");

                PlayerMove.isAnyKeyEnabled = false;　//キー操作を無効にする

                isEnemy1stTouched = true;
            }
            
            if(rigidbody.IsSleeping())
            {
                enemyAnimator.SetFloat("Speed", 0f);
            }
        }

    }
    
    
}
