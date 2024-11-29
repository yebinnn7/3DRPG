using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieControl : MonoBehaviour
{
    [SerializeField]
    private Transform zombieTr;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform playerTr;
    public float attackDist = 0.2f;
    public float traceDist = 20f;

    public int zombieHP;

    public GameObject playerObject; // �÷��̾� ������Ʈ
    private PlayerAttack player; // �÷��̾� ��ũ��Ʈ ����

    public bool zombie_Alive;
    private bool canAttack = true;

    private ZombieGenerator zombieGenerator; // ZombieGenerator ����

    void Awake()
    {
        zombie_Alive = true;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerTr = GameObject.FindWithTag("Player").transform;
        zombieTr = GetComponent<Transform>();

        zombieHP = 100;

        // �÷��̾� ������Ʈ ã��
        if (playerObject == null)
        {
            
            GameObject foundPlayer = GameObject.FindWithTag("Player");
            if (foundPlayer != null)
            {
                playerObject = foundPlayer;
            }
            else
            {
                
                return;
            }
        }

        // PlayerAttack ��ũ��Ʈ ����
        player = playerObject.GetComponent<PlayerAttack>();
        

        // ZombieGenerator ����
        zombieGenerator = FindObjectOfType<ZombieGenerator>();
    }

    void Update()
    {
        if (!zombie_Alive) return;

        float dist = Vector3.Distance(playerTr.position, zombieTr.position);

        if (dist <= attackDist)
        {
            animator.SetBool("IsAttack", true);
            agent.isStopped = true;

            if (canAttack)
            {
                StartCoroutine(PerformAttack());
            }
        }
        else if (dist <= traceDist)
        {
            agent.isStopped = false;
            agent.destination = playerTr.position;
            animator.SetBool("IsTrace", true);
            animator.SetBool("IsAttack", false);
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("IsTrace", false);
        }
    }

    public void TakeDamage(int damage)
    {
        zombieHP -= damage;
        Debug.Log($"���� {damage}�� ���ظ� �Ծ����ϴ�. ���� HP: {zombieHP}");

        if (zombieHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("���� ����߽��ϴ�.");
        zombie_Alive = false;
        animator.SetTrigger("Die");
        agent.isStopped = true;

        if (zombieGenerator != null)
        {
            zombieGenerator.SpawnZombie();
        }

        Destroy(gameObject);
    }

    IEnumerator PerformAttack()
    {
        if (player != null)
        {
            canAttack = false;
            player.AttackedByZombie(10);
            Debug.Log("���� ����");
            yield return new WaitForSeconds(3.0f);
            canAttack = true;
        }
        
    }
}
