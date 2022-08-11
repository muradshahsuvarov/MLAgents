using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public bool BallWasDetected;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Ball"))
        {
            BallWasDetected = true;
        }
    }
}
