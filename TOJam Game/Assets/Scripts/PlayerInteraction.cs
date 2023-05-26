using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{

    Rigidbody rb;
    CharacterController cc;

    bool hitched = false;
    public HingeJoint wagon;
    public float forwardOffset, upOffset;
    public TextMeshProUGUI hitchText;

    bool interactable = false;
    bool holdingBody = false;
    public float forwardHoldPos, upHoldPos;
    public Transform pointer;
    public LayerMask bodyLayer;

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
            RaycastHit hit;
            if(Physics.Raycast(transform.position, pointer.forward, out hit, 3f, bodyLayer))
            {

            }
            holdingBody = !holdingBody;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Body"))
        {
            interactable = true;
        }
    }

    void OntriggerExit(Collider other)
    {
        if(other.CompareTag("Body"))
        {
            interactable = false;
        }
    }
}