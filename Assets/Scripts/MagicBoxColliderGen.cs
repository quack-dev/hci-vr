using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBoxColliderGen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<BoxCollider>() is null)
        {
            gameObject.AddComponent(typeof(BoxCollider));
        }
        Vector3 size = new Vector3(transform.GetComponent<RectTransform>().sizeDelta.x, transform.GetComponent<RectTransform>().sizeDelta.y, 5f);
        gameObject.GetComponent<BoxCollider>().size = size;
        gameObject.GetComponent<BoxCollider>().center = new Vector3(0, -size.y / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
