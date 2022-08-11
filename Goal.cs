using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool iTouchedTheWall;
    public bool iTouchedThePlatform;
    public float velocity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            iTouchedTheWall = true;
        }else if (other.TryGetComponent<Center>(out Center center))
        {
            iTouchedThePlatform = true;
        }
    }
}
