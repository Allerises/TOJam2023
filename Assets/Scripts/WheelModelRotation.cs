using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelModelRotation : MonoBehaviour
{

    public WheelCollider parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 pos;
        Quaternion rot;
        parent.GetWorldPose(out pos, out rot);

        transform.SetPositionAndRotation(pos, rot);
    }
}
