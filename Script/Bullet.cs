using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col){
        Life life = col.gameObject.GetComponent<Life>();

        if(life != null)
            life.Damage(10);
            Destroy (gameObject);
    }
}
