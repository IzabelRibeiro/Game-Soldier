using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Wanger : NetworkBehaviour
{
    public Rigidbody2D prefabBala;
    private const int CIRCLE_DISTANCE = 6;
    private const int CIRCLE_RADIUS = 8;
    private const int ANGLE_CHANGE = 1;

    public float maxSpeed;
    public float rotationSpeed;

    public Vector3 circleCenter;
    public Vector3 displacement;

    public float wanderAngle;

    Vector3 velocity;

    void Start()
    {
        this.velocity = new Vector3 (-1,0,-2);
        this.displacement = new Vector3();
    }

    void Update() {

        int a = Random.Range(0,10);
        if (a%2 == 0){
            CmdFire();
        }

        circleCenter = velocity;
        circleCenter = circleCenter.normalized * CIRCLE_DISTANCE;

        displacement = new Vector3(0,0,-1);
        displacement = displacement.normalized * CIRCLE_RADIUS;
        displacement = setAngle(displacement, wanderAngle);

        wanderAngle += Random.Range(0,2) * ANGLE_CHANGE - ANGLE_CHANGE * 0.5f;

        Vector3 wanderForce = circleCenter + displacement;

        transform.Rotate(0, 0, Random.Range(-1,1) * rotationSpeed * Time.deltaTime);

        velocity = wanderForce * maxSpeed * Time.deltaTime;
        this.transform.position += velocity;
    }

    [Command]
    void CmdFire(){
        Vector3 pos = transform.position + transform.up*1.7f;
        Rigidbody2D bala = (Rigidbody2D)Instantiate(prefabBala, pos, transform.rotation);
        bala.velocity = transform.up*6;
        NetworkServer.Spawn(bala.gameObject);
        Destroy(bala.gameObject,1f);
    }

    Vector3 setAngle(Vector3 vector, float value) {
        float magnitude = vector.magnitude;
        vector.x = Mathf.Cos (value) * magnitude;
        vector.z = Mathf.Sin (value) * magnitude;

        return vector; 
    }
}
