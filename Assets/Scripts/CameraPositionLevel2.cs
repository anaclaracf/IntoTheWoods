using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionLevel2 : MonoBehaviour
{
    Camera m_MainCamera;
    // Start is called before the first frame update
    void Start()
    {
        m_MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (playerPosition.x <= 0.44f)
        {
            m_MainCamera.transform.localPosition = new Vector3(-0.11f, playerPosition.y, transform.localPosition.z);
        }
        else if (playerPosition.x >= 71.5f)
        {
            m_MainCamera.transform.localPosition = new Vector3(71.76f, playerPosition.y, transform.localPosition.z);
        }
        else
        {
            m_MainCamera.transform.localPosition = new Vector3(playerPosition.x, playerPosition.y, transform.localPosition.z);
        }
    }
}
