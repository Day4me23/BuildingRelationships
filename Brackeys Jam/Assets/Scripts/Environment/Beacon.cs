using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : Trigger
{
    [SerializeField]int count = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(transform);
        if (collision.transform.CompareTag("Laser"))
        {
            count++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Laser"))
        {
            count--;
        }
    }
    private void Update()
    {
        if (count > 0)
            active = true;
        else active = false;
    }
}