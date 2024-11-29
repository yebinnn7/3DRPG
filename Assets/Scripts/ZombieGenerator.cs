using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    public GameObject zombiePrefab; // ���� ������
    public Vector3 spawnPoint = new Vector3(4.21f, 0.46f, 0); // ���� ���� ��ġ
    public GameObject playerObject; // �÷��̾� ������Ʈ

    // ���� �����ϴ� �޼���
    public void SpawnZombie()
    {
        if (zombiePrefab == null || playerObject == null)
        {
            
            return;
        }

        // ���� ����
        GameObject newZombie = Instantiate(zombiePrefab, spawnPoint, Quaternion.identity);

        // ������ ���� �÷��̾� ������Ʈ�� �����ϵ��� ����
        ZombieControl zombieControl = newZombie.GetComponent<ZombieControl>();
        if (zombieControl != null)
        {
            zombieControl.playerObject = playerObject; // �÷��̾� ������Ʈ �Ҵ�
        }

        Debug.Log($"���ο� ���� �����Ǿ����ϴ�. ��ġ: {spawnPoint}");
    }
}
