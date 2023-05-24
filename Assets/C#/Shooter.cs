using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireSpeed;
    [SerializeField] private Transform firePoint;

    private void Awake()
    {
        firePoint.localPosition = new Vector2(0.25f, 0.186f);
    }

    public void Shoot(float direction)
    {
        GameObject currentBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
        Rigidbody2D currentBulletVelocity = currentBullet.GetComponent<Rigidbody2D>();

        if (direction >= 0)
        {
            currentBullet.transform.Rotate(0, 0, 0);
            currentBulletVelocity.velocity = new Vector2(fireSpeed * 1, currentBulletVelocity.velocity.y);
        }
        else
        {
            currentBulletVelocity.velocity = new Vector2(fireSpeed * -1, currentBulletVelocity.velocity.y);
            currentBullet.transform.Rotate(0, 180, 0);
        }

    }

    public void SetFirePointPlus()
    {
        firePoint.localPosition = new Vector2(0.25f, 0.186f);
    }
    
    public void SetFirePointMinus()
    {
        firePoint.localPosition = new Vector2(-0.25f, 0.186f);
    }
}