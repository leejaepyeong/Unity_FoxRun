using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Type { enemyA = 10, enemyB = 20};
    public Type type;

    public GameObject child;

    private void Update()
    {
        float ran = Random.Range(80,101);
        child.transform.Rotate(ran * Time.deltaTime, 0, 0,Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            GameManager.instance.Damage((int)type);
        }
    }
}
