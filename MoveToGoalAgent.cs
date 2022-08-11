using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEditor;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material looseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;
    [SerializeField] private float moveSpeed;

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void OnEpisodeBegin()
    {
        targetTransform.localPosition = new Vector3(Random.Range(-5, 5), 2, Random.Range(-5, 5));
        transform.localPosition = new Vector3(Random.Range(-5, 5), 2, Random.Range(-5, 5));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(1f);
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
        }else if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            floorMeshRenderer.material = looseMaterial;
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
}
