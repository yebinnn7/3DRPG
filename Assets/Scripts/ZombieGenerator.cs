using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    public GameObject zombiePrefab; // 좀비 프리팹
    public Vector3 spawnPoint = new Vector3(4.21f, 0.46f, 0); // 좀비 생성 위치
    public GameObject playerObject; // 플레이어 오브젝트

    // 좀비를 생성하는 메서드
    public void SpawnZombie()
    {
        if (zombiePrefab == null || playerObject == null)
        {
            
            return;
        }

        // 좀비 생성
        GameObject newZombie = Instantiate(zombiePrefab, spawnPoint, Quaternion.identity);

        // 생성된 좀비가 플레이어 오브젝트를 참조하도록 설정
        ZombieControl zombieControl = newZombie.GetComponent<ZombieControl>();
        if (zombieControl != null)
        {
            zombieControl.playerObject = playerObject; // 플레이어 오브젝트 할당
        }

        Debug.Log($"새로운 좀비가 생성되었습니다. 위치: {spawnPoint}");
    }
}
