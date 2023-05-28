using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BodyCollection : MonoBehaviour
{
    public TextMeshProUGUI counter, timer;
    int bodyCount;
    public float maxTime;
    float curTime;

    public GameObject text;

    public Transform tree;

    // Start is called before the first frame update
    void Start()
    {
        bodyCount = 0;
        curTime = maxTime;
        text.SetActive(false);
    }

    void Update()
    {
        counter.text = "Bodies Buried: " + bodyCount;
        if(curTime > 0)
            curTime -= Time.deltaTime;
        else
        {
            text.SetActive(true);
            Time.timeScale = 0f;
        }
        timer.text = "Time Remaining: " + Mathf.Ceil(curTime).ToString();

        tree.localScale = new Vector3(1, 1, 1) * bodyCount / 9f;
    }

    void OnTriggerEnter(Collider other)
    {
        Collider[] bodies = Physics.OverlapSphere(transform.position + new Vector3(5, 1, 5), 3);
        List<GameObject> bods = new List<GameObject>();

        foreach(Collider body in bodies)
        {
            if(body.CompareTag("Body") && !bods.Contains(body.gameObject))
            {
                bods.Add(body.gameObject);
            }
        }
        bodyCount = bods.Count;
    }
}
