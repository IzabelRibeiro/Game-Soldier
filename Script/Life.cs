using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Life : NetworkBehaviour
{
    public const int maxLife = 100;
    public bool destroyOnDeath;
    [SyncVar(hook="OnChangeLife")]
    public int life = maxLife;
    public RectTransform lifeBar;

    public void Damage(int damage){
        if(!isServer)
            return;
        life -= damage;
        if (life <= 0) {
            if (destroyOnDeath){
                Destroy(gameObject);
            }else{
                life = maxLife;
                RpcRespawn();
            }
        }
    }
    void OnChangeLife(int life){
        lifeBar.sizeDelta = new Vector2(life, lifeBar.sizeDelta.y);
    }
    [ClientRpc]
    void RpcRespawn(){
        if (isLocalPlayer){
            transform.position = Vector3.zero;
        }
    }
}

