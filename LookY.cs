using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    [SerializeField]
    float sensitivityY = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseLookY();
        
    }

    void MouseLookY()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        Mathf.Clamp(mouseY, 0, float.MaxValue);
        transform.eulerAngles += new Vector3(-mouseY * sensitivityY, 0, 0);
        
    }

    
}
