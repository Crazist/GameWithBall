using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
  public float openDoor = -110f;
  private float speed = 1f;
  public bool canOpen;

    private void Start()
    {
        canOpen = false;
    }
    private void Update()
    {
        if (canOpen)
        {
            Open();
        }

    }
    private void Open()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.x,
            openDoor, transform.rotation.z), speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canOpen = true;
        }
    }
}
