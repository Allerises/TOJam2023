using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{

    Rigidbody rb;
    CharacterController cc;
    public Material rend;

    bool hitched = false;
    public HingeJoint wagon;
    public float forwardOffset, upOffset;
    public TextMeshProUGUI hitchText;

    bool interactable = false;
    bool holdingBody = false;
    public float forwardHoldPos, upHoldPos;
    public Transform pointer;
    public LayerMask bodyLayer;
    Transform heldObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
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
            transform.position = wagon.transform.position + wagon.transform.forward * forwardOffset + wagon.transform.up * upOffset;
            transform.rotation = cc.transform.rotation;
            cc.enabled = true;
        }

        wagon.connectedBody = hitched ? rb : null;
        hitchText.text = hitched ? "Unhitch Wagon" : "Hitch Wagon";
    }

    public void OnInteract()
    {
        if(interactable)
        {
            Debug.Log("trying to grab something");
            
            RaycastHit hit;
            if(Physics.Raycast(pointer.position, pointer.forward, out hit, 10f, bodyLayer) && !holdingBody)
            {
                holdingBody = true;
                heldObject = hit.transform;
                hit.transform.parent = pointer;
                hit.transform.position = transform.position + transform.forward * forwardHoldPos + transform.up * upHoldPos;
                hit.rigidbody.isKinematic = true;
            }
            else if(heldObject)
            {
                holdingBody = false;
                heldObject.parent = null;
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject = null;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Body"))
        {   
            rend.SetColor("_Color", Color.green);
            interactable = true;
        }
    }

    void OntriggerExit(Collider other)
    {
        if(other.CompareTag("Body"))
        {
            rend.SetColor("_Color", Color.green);
            interactable = false;
        }
    }
}