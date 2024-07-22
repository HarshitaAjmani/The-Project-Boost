using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPostion;
    float movementFactor;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    // [SerializeField] [Range(0,1)] float movementFactor;


    // Start is called before the first frame update
    void Start()
    {
        startingPostion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; }

        float cycles = Time.time / period; //cycles will be growing over time

        const float tau = Mathf.PI * 2; // tau mean 360 degree or 2pi radian angle
        float rawSinWave = Mathf.Sin(cycles * tau); //this creats a raw sign wave

        movementFactor = (rawSinWave + 1f) / 2f;   //recalculated to go from 0 to 1 so its cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPostion + offset; 
    }
}
