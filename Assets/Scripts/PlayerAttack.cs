using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int playerHP = 100; // 플레이어의 HP
    public float attackRange = 2.0f; // 공격 범위
    public int attackDamage = 40; // 플레이어의 공격력

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 시
        {
            Attack();
        }
    }

    void Attack()
    {
        // "Zombie" 태그를 가진 모든 좀비 탐색
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("zombie");

        foreach (GameObject zombieObject in zombies)
        {
            float distance = Vector3.Distance(transform.position, zombieObject.transform.position);

            if (distance <= attackRange) // 좀비가 공격 범위 안에 있는지 확인
            {
                ZombieControl zombie = zombieObject.GetComponent<ZombieControl>();
                if (zombie != null)
                {
                    zombie.TakeDamage(attackDamage); // 좀비의 HP를 감소
                    Debug.Log($"좀비에게 {attackDamage} 데미지를 입혔습니다!");
                }
            }
        }
    }


    // 좀비의 공격을 받을 때 호출
    public void AttackedByZombie(int damage)
    {
        playerHP -= damage;
        Debug.Log($"플레이어가 {damage} 데미지를 받았습니다. 현재 HP: {playerHP}");

        if (playerHP <= 0)
        {
            Debug.Log("플레이어가 사망했습니다!");
            
        }
    }
}
