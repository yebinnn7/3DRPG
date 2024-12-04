using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10.0f; // �ӷ�

    public float rotSpeed = 10.0f;
    float gravity = -9.8f; // �߷°� ����
    float yVelocity = 0.0f; // y������ �ۿ��ϴ� �ӵ�

    float jumpPower = 5.0f;

    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // ���� ĳ���Ͱ� �ٴ��� ��� �ִ��� ���� üũ
        // if (cc.collisionFlags == CollisionFlags.Below)
        if (cc.isGrounded == true)
        {
            yVelocity = 0.0f;
        }


        // GetButtonDown: ������ ��
        // GetButton: ������ �ִ� ���� ��� true
        // GetButtonUp: �����ٰ� ���� ��
        // �����̽��ٸ� �������� �������� ����
        if (Input.GetButtonDown("Jump") == true)
        {
            yVelocity = jumpPower;
        }

        Vector3 dir = (h * Vector3.right + v * Vector3.forward);

        if (dir.magnitude >= 0.1f)
        {
            dir.Normalize();
            // transform.forward = dir;
            // �ڿ������� �������� // ���������� ���� ����
            // transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);
        }
        // �߷¼ӵ� ���
        yVelocity += gravity * Time.deltaTime;
        // ĳ���� y���⿡ ����
        dir.y = yVelocity;

        // �̵�����
        // p = p0 + VT (v = s*d)
        // transform.position += speed * (h * transform.right + v * transform.forward).normalized * Time.deltaTime;
        // cc.SimpleMove(speed * (h * transform.right + v * transform.forward).normalized);
        // �ӵ� * �ð�
        // (�ӷ� * ����) * �ð�
        cc.Move(speed * dir * Time.deltaTime);
    }
}
