using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resistor : MonoBehaviour
{
    
    private Rigidbody rigidbodyComponent;
    public SpiceSharp.Components.Resistor resistor;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>(); // shorthand for rigidbody component
    }

    // Update is called once per frame
    void Update()
    {
        // Berkeley Spice
    }
}