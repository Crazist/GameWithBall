using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHero : MonoBehaviour
{
    [SerializeField] 
    private float force = 5f;
    private float distace = 2f;

    private Rigidbody rb;
    private bool isFloor;

    public static bool canShoot { get; set; }

    public delegate void BulletCreate();
    public static event BulletCreate bulletCreate;

    public delegate void ChangeScale();
    public static event ChangeScale changeScale;

    public delegate void ShootBulletEveant();
    public static event ShootBulletEveant shootBulletEveant;

    private void OnEnable()
    {
        SwipeController.SwipeEveant += Onswipe;
    }
    private void OnDisable()
    {
        SwipeController.SwipeEveant -= Onswipe;
    }
    private void Start()
    {
        canShoot = true;
        isFloor = true;
        rb = GetComponent<Rigidbody>();
    }
    private void Onswipe(Vector2 direction)
    {
        if (isFloor)
        {
         Jump(direction);
        }
    }
     private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (transform.localScale.x > 0.1f) { bulletCreate?.Invoke(); }
        }
       if (Input.GetMouseButton(0))
        {
          if(transform.localScale.x > 0.1f) 
            { 
                changeScale?.Invoke();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            shootBulletEveant?.Invoke();
        }
        if (transform.localScale.x <= 0.1f) { Lose(); }

    }
    private void Jump(Vector2 dir)
    {
        canShoot = false;
        isFloor = false;
        rb.AddForce(Vector3.forward * distace, ForceMode.Impulse);
        rb.AddForce(dir * force, ForceMode.Impulse);
    }
     private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Floor"))
        {
          canShoot = true;
          isFloor = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
    private void Lose()
    {
        Debug.Log("Lose");
    }
}
