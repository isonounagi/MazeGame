﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private GameObject enemy;
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

    static public bool isEnemy1stTouched = false;

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
            enemyNavMesh.destination = PlayerMove.playerPos;

            dir = PlayerMove.playerPos - enemyPos;
            float d = dir.magnitude;
            
            enemyAnimator.SetFloat("Speed", enemyMoveSpeed);

            if (d <= 1.3f)
            {
                CameraChange.mainCameraActivate = 0;

                enemyAnimator.SetTrigger("attack1");

                PlayerMove.isAnyKeyEnabled = false;

                isEnemy1stTouched = true;
            }
            
            if(rigidbody.IsSleeping())
            {
                enemyAnimator.SetFloat("Speed", 0f);
            }
        }

    }
    
    
}
