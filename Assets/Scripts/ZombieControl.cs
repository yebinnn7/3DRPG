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

    public GameObject playerObject; // 플레이어 오브젝트
    private PlayerAttack player; // 플레이어 스크립트 참조

    public bool zombie_Alive;
    private bool canAttack = true;

    private ZombieGenerator zombieGenerator; // ZombieGenerator 참조

    void Awake()
    {
        zombie_Alive = true;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerTr = GameObject.FindWithTag("Player").transform;
        zombieTr = GetComponent<Transform>();

        zombieHP = 100;

        // 플레이어 오브젝트 찾기
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

        // PlayerAttack 스크립트 참조
        player = playerObject.GetComponent<PlayerAttack>();
        

        // ZombieGenerator 참조
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
        Debug.Log($"좀비가 {damage}의 피해를 입었습니다. 남은 HP: {zombieHP}");

        if (zombieHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("좀비가 사망했습니다.");
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
            Debug.Log("좀비 공격");
            yield return new WaitForSeconds(3.0f);
            canAttack = true;
        }
        
    }
}
