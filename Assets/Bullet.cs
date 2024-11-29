using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �̵��������� �̵�
    // �չ���(forward)
    public float speed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5.0f); // 5�� �� �ı�
    }

    // Update is called once per frame
    void Update()
    {
        // p = p0 + s*d*t;
        transform.position += speed * transform.forward * Time.deltaTime;
    }
}
