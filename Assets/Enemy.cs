using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    CharacterController cc;

    float rotSpeed = 10.0f;

    Transform targetTr;

    // 상태에 대한 정의
    enum EnemyState
    {
        Idle,
        Move,
        Attack
    }

    EnemyState state;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        targetTr = GameObject.Find("Player").transform;
        state = EnemyState.Idle; // 최초 상태 정의
    }

    // Update is called once per frame
    void Update()
    {
        // 상태에 대한 목록
        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;

            // 이하 상태 추가
        }

        
    }

    // 각 상태마다의 함수
    float currentTime = 0.0f;
        

    // 정지상태
    // 2초 뒤에 Move상태로 이동
    void Idle()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 2.0f) // condition(조건)
        {
            // 상태전이
            state = EnemyState.Move;
            currentTime = 0.0f;
        }
    }

    void Move()
    {
        // s*d
        // 벡터A - 벡터B 는 A <- B 방향
        Vector3 dir = targetTr.position - transform.position;

        if (dir.magnitude < 2.0f) 
        {
            state = EnemyState.Attack;
            currentAttackTime = 2.0f; // 2초 지연 공격에 대한 해결
        }

        dir.Normalize();
        dir.y = 0;

        // 자연스러운 선형보간 // 짐벌락현상에 문제 있음
        // transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * rotSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);

        cc.SimpleMove(speed * dir);
    }

    // 2초에 한번씩 공격
    float currentAttackTime = 0.0f;
    void Attack()
    {
        currentAttackTime += Time.deltaTime;
        if (currentAttackTime > 2.0f)
        {
            print("공격");
            currentAttackTime = 0.0f;
        }

        // 거리가 2M를 벗어날 경우 Move 상태전이
        // s*d
        // 벡터A - 벡터B 는 A <- B 방향
        Vector3 dir = targetTr.position - transform.position;

        if (dir.magnitude > 2.0f + 0.5f) // 0.5f: 50cm의 정도의 여유를 줘야함 (Move와 Attack이 왔다갔다 하는 경우가 발생)
        {
            state = EnemyState.Move;
        }

    }
}
