using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10.0f; // 속력

    public float rotSpeed = 10.0f;
    float gravity = -9.8f; // 중력값 설정
    float yVelocity = 0.0f; // y쪽으로 작용하는 속도

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

        // 현재 캐릭터가 바닥을 밟고 있는지 여부 체크
        // if (cc.collisionFlags == CollisionFlags.Below)
        if (cc.isGrounded == true)
        {
            yVelocity = 0.0f;
        }


        // GetButtonDown: 눌렀을 때
        // GetButton: 누르고 있는 동안 계속 true
        // GetButtonUp: 눌렀다가 뗐을 때
        // 스페이스바를 눌렀을때 점프동작 연출
        if (Input.GetButtonDown("Jump") == true)
        {
            yVelocity = jumpPower;
        }

        Vector3 dir = (h * Vector3.right + v * Vector3.forward);

        if (dir.magnitude >= 0.1f)
        {
            dir.Normalize();
            // transform.forward = dir;
            // 자연스러운 선형보간 // 짐벌락현상에 문제 있음
            // transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);
        }
        // 중력속도 계산
        yVelocity += gravity * Time.deltaTime;
        // 캐릭터 y방향에 적용
        dir.y = yVelocity;

        // 이동공식
        // p = p0 + VT (v = s*d)
        // transform.position += speed * (h * transform.right + v * transform.forward).normalized * Time.deltaTime;
        // cc.SimpleMove(speed * (h * transform.right + v * transform.forward).normalized);
        // 속도 * 시간
        // (속력 * 방향) * 시간
        cc.Move(speed * dir * Time.deltaTime);
    }
}
