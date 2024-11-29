using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10.0f; // �ӷ�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // �̵�����
        // p = p0 + VT (v = s*d)
        transform.position += speed * (h * transform.right + v * transform.forward).normalized * Time.deltaTime;
    }
}
