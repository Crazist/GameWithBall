using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabBullet : MonoBehaviour
{
    
    float infectionScale = 0.0005f;
    private float radius = 0.2f;
    private Vector3 curLocalScale;

    private void Start()
    {
        curLocalScale = GetComponent<Transform>().localScale;
        curLocalScale = transform.localScale;
    }
    private void Update()
    {

        if(transform.localScale.x > curLocalScale.x)
        {
            curLocalScale = transform.localScale;
            radius += infectionScale;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var hit in hitColliders)
            {
                if (hit.gameObject.CompareTag("Enemy"))
                {
                    hit.gameObject.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
                    Destroy(hit.gameObject, 1.5f);
                    Destroy(gameObject, 1.5f);
                    
                }
            }
        }
        else
        {
            StartCoroutine(DestroyPrefab());
        }
    }

    private IEnumerator DestroyPrefab()
    {
        if (gameObject != null)
        {
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        } 
    }
    
}
