using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") == true)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = firePos.position;
            bullet.transform.forward = firePos.forward;
        }
    }
}
