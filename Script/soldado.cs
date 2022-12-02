using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class soldado : NetworkBehaviour{
    public Animator animator;
    public Rigidbody2D prefabBala;
    
    
    void Update(){
        if(!isLocalPlayer)
        return;

        if(Input.GetKeyDown(KeyCode.Space)){
            CmdFire();
        }
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        transform.Rotate(0,0, - Input.GetAxis("Horizontal") * 180 * Time.deltaTime);
        transform.Translate(0, Input.GetAxis("Vertical") * 4 * Time.deltaTime, 0);


    }
    [Command]
    void CmdFire(){
        Vector3 pos = transform.position + transform.up*1.7f;
        Rigidbody2D bala = (Rigidbody2D)Instantiate(prefabBala, pos, transform.rotation);
        bala.velocity = transform.up*6;
        NetworkServer.Spawn(bala.gameObject);
        Destroy(bala.gameObject,1f);
    }
     public override void OnStartLocalPlayer(){
        GetComponent<SpriteRenderer>().color = Color.red;
    }
     
}



