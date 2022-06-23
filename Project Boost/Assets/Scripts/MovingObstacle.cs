using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float movementFactor;
    [SerializeField] private float period = 2f;

    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period == 0)
            return;
        float cycles = Time.time / period; // handles cycles of sinus

        const float tau = Mathf.PI * 2; //double pi aka tau
        float rawSinWave = Mathf.Sin(cycles * tau); //gives you number from -1 to 1 in given period

        //Debug.Log(rawSinWave);

        movementFactor = (rawSinWave + 1f) / 2f; // gives you 0 to 1 based on rawSinWave point

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
