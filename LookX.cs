using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{
    [SerializeField]
    float sensitivityX=1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseLookX();
    }

    void MouseLookX()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.localEulerAngles += new Vector3(0, mouseX * sensitivityX, 0);

    }
}
