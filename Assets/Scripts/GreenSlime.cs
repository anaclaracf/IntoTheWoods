using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlime : MonoBehaviour
{
    private Vector3 dir = Vector3.left;
    public int speed = 5;
    public int x_inicial = 0;
    public int x_final = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir*speed*Time.deltaTime);
 
        if(transform.position.x <= x_inicial){
            dir = Vector3.right;
        }else if(transform.position.x >= x_final){
            dir = Vector3.left;
        }
    }
}
