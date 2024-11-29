using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int playerHP = 100; // �÷��̾��� HP
    public float attackRange = 2.0f; // ���� ����
    public int attackDamage = 40; // �÷��̾��� ���ݷ�

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ�� ��
        {
            Attack();
        }
    }

    void Attack()
    {
        // "Zombie" �±׸� ���� ��� ���� Ž��
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("zombie");

        foreach (GameObject zombieObject in zombies)
        {
            float distance = Vector3.Distance(transform.position, zombieObject.transform.position);

            if (distance <= attackRange) // ���� ���� ���� �ȿ� �ִ��� Ȯ��
            {
                ZombieControl zombie = zombieObject.GetComponent<ZombieControl>();
                if (zombie != null)
                {
                    zombie.TakeDamage(attackDamage); // ������ HP�� ����
                    Debug.Log($"���񿡰� {attackDamage} �������� �������ϴ�!");
                }
            }
        }
    }


    // ������ ������ ���� �� ȣ��
    public void AttackedByZombie(int damage)
    {
        playerHP -= damage;
        Debug.Log($"�÷��̾ {damage} �������� �޾ҽ��ϴ�. ���� HP: {playerHP}");

        if (playerHP <= 0)
        {
            Debug.Log("�÷��̾ ����߽��ϴ�!");
            
        }
    }
}
