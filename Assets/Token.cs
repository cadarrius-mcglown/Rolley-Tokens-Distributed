using UnityEngine;
using System.Collections;

public class Token : MonoBehaviour {

    // Use this for initialization
    void OnCollisionEnter()
    {
        Debug.Log("hit");
        //var hit = collision.gameObject;
        //var health = hit.GetComponent<Health>();
        //if (health != null)
        //{
        //    health.TakeDamage(10);
        //}

        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject;

        Debug.Log("hit");
        Destroy(gameObject);
    }
}
