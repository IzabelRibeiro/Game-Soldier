using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocidade : MonoBehaviour
{
  public Vector2 velocity;
  

    void FixedUpdate()
    {
        Rigidbody2D rb= GetComponent<Rigidbody2D>();
        rb.velocity = Quaternion.Euler(0,0,rb.rotation+180) * velocity;
    }
}
