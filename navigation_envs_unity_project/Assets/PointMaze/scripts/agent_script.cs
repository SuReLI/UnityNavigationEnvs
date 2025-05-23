using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


public class TurtleAgent : Agent {
    [SerializeField] private Transform _goal;
    [SerializeField] private float _moveSpeed = 1.5f;
    [SerializeField] private float _rotationSpeed = 180f;

    private Renderer _renderer;

    private int _currentEpisode = 0;
    private float _cumulativeReward = 0f;
    private bool _goalReached = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Initialize() {
        Debug.Log("Initialize()");

        // Get the agent's body, and extract the renderer from it.
        Transform bodyTransform = transform.Find("body");
        if (bodyTransform != null)
        {
            _renderer = bodyTransform.GetComponent<Renderer>();
            if (_renderer == null)
            {
                Debug.LogError("Renderer not found on 'body' child!");
            }
        }
        else
        {
            Debug.LogError("'body' child not found!");
        }


        // _renderer = GetComponent<Renderer>();
        _currentEpisode = 0;
        _cumulativeReward = 0f;
        _goalReached = false;
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("OnEpisodeBegin()");
        _currentEpisode++;
        _goalReached = false;
        _cumulativeReward = 0f;
        _renderer.material.color = Color.blue;

        SpawnObjects();
    }

    private void SpawnObjects()
    {
        transform.localRotation = Quaternion.identity;
        transform.localPosition = new Vector3(0f, 0.15f, 0f);

        // Randomize the direction on the Y-axis (angle in degrees)
        float randomAngle = Random.Range(0f, 360f);
        Vector3 randomDirection = Quaternion.Euler(0f, randomAngle, 0f) * Vector3.forward;

        // Randomize the distance within the range [1, 2.5]
        float randomDistance = Random.Range(1f, 2.5f);

        // Calculate the goal's position
        Vector3 goalPosition = transform.localPosition + randomDirection * randomDistance;

        // Apply the calculated position to the goal
        _goal.localPosition = new Vector3(goalPosition.x, 0.75f, goalPosition.z);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // The goal's position
        float goalPosX_normalized = _goal.localPosition.x / 5f;
        float goalPosZ_normalized = _goal.localPosition.z / 5f;

        // The turtle's position
        float turtlePosX_normalized = transform.localPosition.x / 5f;
        float turtlePosZ_normalized = transform.localPosition.z / 5f;

        // The turtle's direction (on the Y Axis)
        float turtleRotation_normalized = (transform.localRotation.eulerAngles.y / 360f) * 2f - 1f;

        sensor.AddObservation(goalPosX_normalized);
        sensor.AddObservation(goalPosZ_normalized);
        sensor.AddObservation(turtlePosX_normalized);
        sensor.AddObservation(turtlePosZ_normalized);
        sensor.AddObservation(turtleRotation_normalized);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        MoveAgent(actions.DiscreteActions);

        // Only penalize if goal wasn't reached this step
        if (_goalReached)
        {

            EndEpisode();
        }
        else
        {
            // Penalty given each step to encourage agent to finish task quickly.
            AddReward(-1f / MaxStep);
        }

        // Update the cumulative reward after adding the step penalty
        _cumulativeReward = GetCumulativeReward();
    }

    public void MoveAgent(ActionSegment<int> act)
    {
        var action = act[0];

        switch (action)
        {
            case 1: // Move forward
                transform.position += transform.forward * _moveSpeed * Time.deltaTime;
                break;
            case 2: // Rotate left
                transform.Rotate(0f, -_rotationSpeed * Time.deltaTime, 0f);
                break;
            case 3: // Rotate right
                transform.Rotate(0f, _rotationSpeed * Time.deltaTime, 0f);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("goal"))
        {
            _goalReached = true;
            _renderer.material.color = Color.green;
        }
    }
}
