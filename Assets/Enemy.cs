using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    CharacterController cc;

    float rotSpeed = 10.0f;

    Transform targetTr;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        targetTr = GameObject.Find("Player").transform; 
    }

    // Update is called once per frame
    void Update()
    {
        // s*d
        // 벡터A - 벡터B 는 A <- B 방향
        Vector3 dir = targetTr.position - transform.position;
        dir.Normalize();

        // 자연스러운 선형보간
        transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * rotSpeed);

        cc.SimpleMove(speed * dir);
    }
}
