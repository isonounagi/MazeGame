using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove_2nd : MonoBehaviour
{
    private GameObject enemy2nd;
    private Vector3 enemyPos;
    private CharacterController enemyController;
    private Rigidbody rigidbody;

    private Vector3 dir;
    private GameObject mainCamera;
    private GameObject otherCamera;
    private GameObject zoomCamera;

    private Animator enemyAnimator;

    private NavMeshAgent enemyNavMesh;

    [SerializeField]
    private float enemyMoveSpeed;

    static public bool isEnemy2ndTouched = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<CharacterController>();
        enemyNavMesh = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        mainCamera = GameObject.Find("Main Camera");
        otherCamera = GameObject.Find("Camera");
        zoomCamera = GameObject.Find("ZoomCamera");
    }

    // Update is called once per frame
    void Update()
    {
        enemy2nd = GameObject.FindGameObjectWithTag("Enemy2nd");
        enemyPos = enemy2nd.transform.position;

        if (PlayerMove.playerPos != null)
        {
            enemyNavMesh.destination = PlayerMove.playerPos;

            dir = PlayerMove.playerPos - enemyPos;
            float d = dir.magnitude;

            enemyAnimator.SetFloat("Speed", enemyMoveSpeed);

            if (d <= 1.3f)
            {
                CameraChange.mainCameraActivate = 0;

                enemyAnimator.SetTrigger("attack1");

                PlayerMove.isAnyKeyEnabled = false;

                isEnemy2ndTouched = true;
            }

            if (rigidbody.IsSleeping())
            {
                enemyAnimator.SetFloat("Speed", 0f);
            }
        }
    }
}
