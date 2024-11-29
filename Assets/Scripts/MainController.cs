using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public Animator mAnimator;
    public Rigidbody mRigidbody;

    private float h;
    private float v;

    private float moveX;
    private float moveZ;
    private float speedH = 50f;
    private float speedZ = 80f;


    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mAnimator.Play("JUMP00", -1, 0);
        }

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        mAnimator.SetFloat("h", h);
        mAnimator.SetFloat("v", v);

        moveX = h * speedH * Time.deltaTime;
        moveZ = v * speedZ * Time.deltaTime;

        if (moveZ <= 0)
        {
            moveX = 0;
        }
        mRigidbody.velocity = new Vector3(moveX, 0, moveZ);
    }
}
