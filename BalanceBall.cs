using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class BalanceBall : Agent
{
    [SerializeField] private Transform target;
    [SerializeField] private int rotationSpeed;
    [SerializeField] private GameObject wall;
    [SerializeField] GameObject touchedObject;
    [SerializeField] MeshRenderer mainMeshRenderer;
    [SerializeField] Material winMaterial;
    [SerializeField] Material looseMaterial;

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(target.gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude);
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(transform.localRotation);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float X = actions.ContinuousActions[0] * Time.deltaTime * rotationSpeed;
        float Z = actions.ContinuousActions[1] * Time.deltaTime * rotationSpeed;

        transform.Rotate(X,0f,Z);
    }

    public override void OnEpisodeBegin()
    {
        touchedObject = null;
        wall.GetComponent<Wall>().BallWasDetected = false;
        target.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        target.transform.localPosition = new Vector3(Random.Range(-.6f,.6f),1f,Random.Range(-.7f,.7f));
        transform.rotation = Quaternion.Euler(0f,0f,0f);
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            touchedObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (touchedObject != null)
        {
            touchedObject = null;
        }
    }

    private void Update()
    {
            if (target.gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude <= 0.01f 
            && touchedObject != null && touchedObject.TryGetComponent<Goal>(out Goal goal))
            {
                Debug.Log("Ball stopped");
                SetReward(1);
                mainMeshRenderer.material = winMaterial;
                EndEpisode();
            }


        if (wall.GetComponent<Wall>().BallWasDetected)
        {
            Debug.Log("Ball touched the wall");
            SetReward(-1);
            mainMeshRenderer.material = looseMaterial;
            EndEpisode();
        }
    }
}
