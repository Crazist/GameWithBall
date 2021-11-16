using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    public GameObject bullet;
    [SerializeField]
    public GameObject Road;

    private Vector3 ScaleChange;
    private float offset = 0.9f;
    private float speed = 15f;
    private  GameObject curBullet;
    private float minChange = 0.0001f;
    
private void OnEnable()
    {
        BallHero.bulletCreate += InstatiateBullet;
        BallHero.changeScale += ChangeScale;
        BallHero.shootBulletEveant += Force;
    }
    private void OnDisable()
    {
        BallHero.bulletCreate -= InstatiateBullet;
        BallHero.changeScale -= ChangeScale;
        BallHero.shootBulletEveant -= Force;
    }
    private void Start()
    {
        ScaleChange = new Vector3(minChange, minChange, minChange);
    }

    private void Force()
    {
        curBullet.GetComponent<Rigidbody>().isKinematic = false;
        curBullet.GetComponent<Rigidbody>().AddForce(Vector3.forward * speed, ForceMode.Impulse);
    }

    private void InstatiateBullet()
    {
        curBullet = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z + offset), Quaternion.identity);
    }
    private void ChangeScale()
    {
        curBullet.transform.localScale += ScaleChange;
        gameObject.transform.localScale -= ScaleChange;
        Road.transform.localScale = new Vector3(Road.transform.localScale.x - ScaleChange.x / 4, Road.transform.localScale.y, Road.transform.localScale.z);
    }

  

}
