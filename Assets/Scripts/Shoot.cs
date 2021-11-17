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
    private float minChange = 0.0002f;
    
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
        if (curBullet != null)
        {
            curBullet.GetComponent<Rigidbody>().isKinematic = false;
            curBullet.GetComponent<Rigidbody>().AddForce(Vector3.forward * speed, ForceMode.Impulse);
        }
    }

    private void InstatiateBullet()
    {
            StartCoroutine(Create());
    }
    private void ChangeScale()
    {
        if(curBullet != null)
        {
            curBullet.transform.localScale += ScaleChange;
            gameObject.transform.localScale -= ScaleChange;
            Road.transform.localScale = new Vector3(Road.transform.localScale.x - ScaleChange.x / 4, Road.transform.localScale.y, Road.transform.localScale.z);
        }
    }
    IEnumerator Create()
    {
        yield return new WaitForSeconds(0.05f);
        if (BallHero.canShoot)
        {
            curBullet = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z + offset), Quaternion.identity);
        }
    }
}
  


