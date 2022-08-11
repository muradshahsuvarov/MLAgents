using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    public bool ballIsInTheCenter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            ballIsInTheCenter = true;
        }
    }

    private void Update()
    {

    }
}
