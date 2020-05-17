using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uWintab;

public class SigilGenerator : MonoBehaviour
{
    public GameObject sigilPrefab;
    public Pen pen;

    GameObject model;
    // Start is called before the first frame update
    void Start()
    {
        model = Instantiate(sigilPrefab, transform);
        model.transform.localScale = new Vector3(.2f, .2f, .2f);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        pen = GameObject.Find("RealStuff/Tablet/PenSpace/Pen").GetComponent<Pen>();
    }

    public void OnClick()
    {
        GameObject newSigil = Instantiate(sigilPrefab);
        pen.Drag(newSigil);
        pen.GoToRitualFrame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
