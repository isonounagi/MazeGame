using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{   /* 
    // 移動方向、ベクトル
    private Vector3 m_direction;
    private Vector3 m_move_vector;
    private Vector3 m_current_grid;

    private Animator enemyAnimator;

    //  移動方向の候補を参考にして移動可能な方向を得る.
    //  引数:
    //  first   第一候補.
    //  second  第二候補.
    //  戻り値:
    //  移動方向/ 移動不可能ならVector3.zero
    private Vector3 DirectionChoice(Vector3 first, Vector3 second)
    {
        // 第一候補.
        // 第二候補.
        // 第二候補の逆方向.
        // 第ー候補の逆方向.
        //
        // の順番に調べて、移動可能ならその方向を返す.

        // 第一候補.
        if (!IsReverseDirection(first) &&
            !CheckWall(first))
            return first;

        // 第二候補.
        if (IsReverseDirection(second) &&
            CheckWall(second))
            return second;

        first *= -1.0f;
        second *= -1.0f;
        // 第二候補の逆方向.
        if (IsReverseDirection(second) &&
            CheckWall(second))
            return second;
        // 第ー候補の逆方向.
        if (IsReverseDirection(first) &&
            CheckWall(first))
            return first;

        return Vector3.zero;
    }

    public void Move(float t)
    {
        // 次に移動する位置.
        Vector3 pos = transform.position;
        pos += m_direction * 1.0f * t;


        // グリッド上を通過したかチェック.
        bool across = false;

        // 整数化した値が違っていた場合、グリッドの境界をまたいだ.
        if ((int)pos.x != (int)transform.position.x)
            across = true;
        if ((int)pos.z != (int)transform.position.z)
            across = true;

        Vector3 near_grid = new Vector3(Mathf.Round(pos.x), pos.y, Mathf.Round(pos.z));
        m_current_grid = near_grid;
        // 正面の壁にぶつかったか.
        Vector3 forward_pos = pos + m_direction * 0.5f; // 半Unit先までRayを飛ばしてみる.
        if (Mathf.RoundToInt(forward_pos.x) != Mathf.RoundToInt(pos.x) ||
            Mathf.RoundToInt(forward_pos.z) != Mathf.RoundToInt(pos.z))
        {
            Vector3 tpos = pos;
            tpos.y += 0.5f;
            bool collided = Physics.Raycast(tpos, m_direction, 1.0f, 1 << 0);
            if (collided)
            {
                pos = near_grid;
                across = true;
            }
        }
        if (across || (pos - near_grid).magnitude < 0.00005f)
        {
            Vector3 direction_save = m_direction;

            

            if (Vector3.Dot(direction_save, m_direction) < 0.00005f)
                pos = near_grid + m_direction * 0.001f;  // 少し動かしておかないと再びOnGridするので.
        }

        m_move_vector = (pos - transform.position) / t;
        transform.position = pos;
    }

    public void SetDirection(Vector3 v)
    {
        m_direction = v;
    }

    public Vector3 GetDirection()
    {
        return m_direction;
    }

    public bool IsReverseDirection(Vector3 v)
    {
        if (Vector3.Dot(v, m_direction) < -0.99999f)
            return true;
        else
            return false;
    }

    public bool CheckWall(Vector3 direction)
    {
        Vector3 tpos = m_current_grid;
        tpos.y += 0.5f;
        return Physics.Raycast(tpos, direction, 1.0f, 1 << 0);
    }

    private void Tracer(Vector3 newPos)
    {
        Vector3 newDirection1, newDirection2;
        Vector3 diff = PlayerMove.playerPos - newPos;

        if(Mathf.Abs(diff.x) > Mathf.Abs(diff.z))
        {
            newDirection1 = new Vector3(1, 0, 0) * Mathf.Sign(diff.x);
            newDirection2 = new Vector3(0, 0, 1) * Mathf.Sign(diff.z);
        }
        else
        {
            newDirection2 = new Vector3(1, 0, 0) * Mathf.Sign(diff.x);
            newDirection1 = new Vector3(0, 0, 1) * Mathf.Sign(diff.z);
        }

        Vector3 newDir = DirectionChoice(newDirection1, newDirection2);

    }

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Tracer(PlayerMove.playerPos);

        Move(Time.deltaTime);
    }
    */

    private GameObject enemy;
    private Vector3 enemyPos;
    private CharacterController enemyController;
    private Rigidbody rigidbody;

    private Animator enemyAnimator;

    private NavMeshAgent enemyNavMesh;

    [SerializeField]
    private float enemyMoveSpeed;

    private void Start()
    {
        enemyController = GetComponent<CharacterController>();
        enemyNavMesh = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        enemy = GameObject.Find("troll_");
        enemyPos = enemy.transform.position;

        /*
        Ray rayForward = new Ray(enemyPos, transform.TransformDirection(Vector3.forward));

        RaycastHit hit;

        if (Physics.Raycast(rayForward, out hit, 0.5f))
        {
            int random = Random.Range(0, 2);
            if(random == 0)
            {
                enemy.transform.Rotate(new Vector3(0, 90, 0));
            }
            else
            {
                enemy.transform.Rotate(new Vector3(0, -90, 0));
            }
                 
        }
        else
        {
            enemyController.Move(this.gameObject.transform.forward * enemyMoveSpeed * Time.deltaTime);
        }
        */

        if (PlayerMove.playerPos != null)
        {
            enemyNavMesh.destination = PlayerMove.playerPos;
            
            enemyAnimator.SetFloat("Speed", enemyMoveSpeed);
            
            if(rigidbody.IsSleeping())
            {
                enemyAnimator.SetFloat("Speed", 0f);
            }
        }

    }
    
    
}
