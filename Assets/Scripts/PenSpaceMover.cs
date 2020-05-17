using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenSpaceMover : MonoBehaviour
{
    public Vector3 desiredLocation;
    public Vector3 desiredScale;
    public Quaternion desiredRotation;
    public Vector3 desiredPenScale;
    public float speed;
    public float rescaleSpeed;
    public float penRescaleSpeed;
    public float rotationSpeed;
    public GameObject PenModel;

    Vector3 rotationDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        desiredLocation = transform.position;
        desiredScale = transform.localScale;
        desiredRotation = transform.rotation;
        desiredPenScale = PenModel.transform.localScale;
    }

    public void NewOrientation(Vector3 desiredLocation_, Vector3 desiredScale_, Vector3 desiredEulerRotation_, Vector3 desiredPenScale_, float time)
    {
        desiredLocation = desiredLocation_;
        desiredScale = desiredScale_;
        desiredPenScale = desiredPenScale_;
        desiredRotation = Quaternion.Euler(desiredEulerRotation_);

        float distance = Vector3.Distance(desiredLocation, transform.position);
        float rescaleDistance = Vector3.Distance(desiredScale, transform.localScale);
        float penRescaleDistance = Vector3.Distance(desiredPenScale, PenModel.transform.localScale);

        speed = distance / time;
        rescaleSpeed = rescaleDistance / time;
        penRescaleSpeed = penRescaleDistance / time;
    }

    public void NewLocalOrientation(Vector3 desiredLocation_, Vector3 desiredScale_, Vector3 desiredEulerRotation_, Vector3 desiredPenScale_, float time)
    {
        NewOrientation(transform.parent.TransformPoint(desiredLocation_), desiredScale_, transform.parent.rotation * desiredEulerRotation_, desiredPenScale_, time);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, desiredLocation, Time.deltaTime * speed);
        //float distance = Vector3.Distance(desiredLocation, transform.position);
        //float distanceThisTick = Time.deltaTime * speed;
        //if (distance >= Mathf.Abs(distanceThisTick))
        //{
        //    Vector3 direction = (desiredLocation - transform.position).normalized;
        //    transform.position += direction * distanceThisTick;
        //}

        transform.localScale = Vector3.MoveTowards(transform.localScale, desiredScale, Time.deltaTime * rescaleSpeed);
        //float rescaleDistance = Vector3.Distance(desiredScale, transform.localScale);
        //float scaleThisTick = Time.deltaTime * rescaleSpeed;
        //if (rescaleDistance > Mathf.Abs(scaleThisTick))
        //{
        //    Vector3 direction = (desiredScale - transform.localScale).normalized;
        //    transform.localScale += direction * scaleThisTick;
        //}

        PenModel.transform.localScale = Vector3.MoveTowards(PenModel.transform.localScale, desiredPenScale, Time.deltaTime * penRescaleSpeed);

        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }
}
