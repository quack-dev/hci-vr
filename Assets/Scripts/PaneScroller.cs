using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uWintab;

public class PaneScroller : MonoBehaviour
{
    public Tablet _tablet;
    public float velocity;
    // Start is called before the first frame update
    void Start()
    {
        _tablet = GameObject.Find("RealStuff/Tablet").GetComponent<Tablet>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_tablet.GetExpKey(0))
        {
            transform.position += Vector3.up * velocity * Time.deltaTime;
        }
        if (_tablet.GetExpKey(1))
        {
            transform.position -= Vector3.up * velocity * Time.deltaTime;
        }
    }
}
