using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using StarterAssets;

public class PlayerInteraction : MonoBehaviour
{

    Rigidbody rb;
    CharacterController cc;
    public Image crosshair;

    bool hitched = false;
    public HingeJoint hitch, wagonJoint;
    public Rigidbody wagon;
    public float forwardOffset, upOffset;
    public Transform wagonBase;
    public TextMeshProUGUI hitchText;

    bool holdingBody = false;
    public float forwardHoldPos, upHoldPos;
    public Transform pointer;
    public LayerMask bodyLayer;
    Transform heldObject;
    public TextMeshProUGUI bodyText;

    public WheelCollider wheel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        
        wheel.ConfigureVehicleSubsteps(2, 50, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHitch()
    {
        hitched = !hitched;

        if(hitched)
        {
            cc.enabled = false;
            hitch.transform.rotation = Quaternion.identity;
            hitch.transform.Translate(new Vector3(0, upOffset, 0));
            transform.position = hitch.transform.position + hitch.transform.forward * forwardOffset - hitch.transform.up * upOffset;
            cc.enabled = true;
            wagonJoint.connectedBody = null;
            StartCoroutine(UnparentBodies());
        }

        hitch.connectedBody = hitched ? rb : null;
        hitchText.text = hitched ? "Unhitch Wagon" : "Hitch Wagon";
    }

    public void OnInteract()
    {
        RaycastHit hit;
        if(Physics.Raycast(pointer.position, pointer.forward, out hit, 10f, bodyLayer) && !holdingBody)
        {
            GetComponent<ThirdPersonController>().MoveSpeed = 1f;
            holdingBody = true;
            heldObject = hit.transform;
            hit.transform.parent = pointer;
            hit.transform.position = pointer.position + pointer.forward.normalized * forwardHoldPos + pointer.up * upHoldPos;
            hit.rigidbody.isKinematic = true;
        }
        else if(holdingBody)
        {
            GetComponent<ThirdPersonController>().MoveSpeed = 2f;
            holdingBody = false;
            heldObject.parent = null;
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject = null;
        }
        bodyText.text = holdingBody ? "Drop Body" : "Grab Body";
    }

    public void OnResetWagon()
    {
        if(hitched)
        {
            OnHitch();
        }
        wagon.transform.position = wagon.transform.position + Vector3.up;
        hitch.transform.position = wagon.transform.position + wagon.transform.forward.normalized * 1.75f + Vector3.down;
        wagon.transform.rotation = Quaternion.identity;
    }

    IEnumerator UnparentBodies()
    {
        yield return new WaitForSeconds(.1f);
        hitch.transform.position = wagon.transform.position + wagon.transform.forward.normalized * 1.75f;
        wagonJoint.connectedBody = hitch.GetComponent<Rigidbody>();
    }
}