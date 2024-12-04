using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    CharacterController cc;

    float rotSpeed = 10.0f;

    Transform targetTr;

    // ���¿� ���� ����
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
        state = EnemyState.Idle; // ���� ���� ����
    }

    // Update is called once per frame
    void Update()
    {
        // ���¿� ���� ���
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

            // ���� ���� �߰�
        }

        
    }

    // �� ���¸����� �Լ�
    float currentTime = 0.0f;
        

    // ��������
    // 2�� �ڿ� Move���·� �̵�
    void Idle()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 2.0f) // condition(����)
        {
            // ��������
            state = EnemyState.Move;
            currentTime = 0.0f;
        }
    }

    void Move()
    {
        // s*d
        // ����A - ����B �� A <- B ����
        Vector3 dir = targetTr.position - transform.position;

        if (dir.magnitude < 2.0f) 
        {
            state = EnemyState.Attack;
            currentAttackTime = 2.0f; // 2�� ���� ���ݿ� ���� �ذ�
        }

        dir.Normalize();
        dir.y = 0;

        // �ڿ������� �������� // ���������� ���� ����
        // transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * rotSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);

        cc.SimpleMove(speed * dir);
    }

    // 2�ʿ� �ѹ��� ����
    float currentAttackTime = 0.0f;
    void Attack()
    {
        currentAttackTime += Time.deltaTime;
        if (currentAttackTime > 2.0f)
        {
            print("����");
            currentAttackTime = 0.0f;
        }

        // �Ÿ��� 2M�� ��� ��� Move ��������
        // s*d
        // ����A - ����B �� A <- B ����
        Vector3 dir = targetTr.position - transform.position;

        if (dir.magnitude > 2.0f + 0.5f) // 0.5f: 50cm�� ������ ������ ����� (Move�� Attack�� �Դٰ��� �ϴ� ��찡 �߻�)
        {
            state = EnemyState.Move;
        }

    }
}
