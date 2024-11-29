using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 이동공식으로 이동
    // 앞방향(forward)
    public float speed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5.0f); // 5초 뒤 파괴
    }

    // Update is called once per frame
    void Update()
    {
        // p = p0 + s*d*t;
        transform.position += speed * transform.forward * Time.deltaTime;
    }
}
