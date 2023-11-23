using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform gunTransform;
    public GameObject bulletPrefab;
    public float bulletSpeed = 3f;
    public Vector2 bulletDirection = Vector2.right;

    void Update()
    {
        if(!GameManager.instance.isLive)
            return;
            
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePosition - gunTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
        bullet.transform.Rotate(0, 0, -90);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = gunTransform.right * bulletSpeed;
        
        Destroy(bullet, 2.0f); // 5 seconds

    }
}
