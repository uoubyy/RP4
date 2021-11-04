using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCController : MonoBehaviour
{
    public Transform m_targetObject; // the player

    public float m_rotationSpeed = 0.5f;
    private NavMeshAgent m_navMeshAgent;
    private Vector3 m_tergetDirection;
    private Vector3 m_currentDirection = Vector3.zero;

    private int m_axiousLevel = 1;
    private float[] m_minRadiusArr = new float[] {10.0f, 8.0f, 6.0f, 4.0f, 2.0f};
    private float[] m_maxRadiusArr = new float[] {20.0f, 10.0f, 8.0f, 6.0f, 4.0f};

    private float m_minRadius;
    private float m_maxRadius;

    private void Start()
    {
        m_axiousLevel = Mathf.Max(SliderVariable.NPCRadius, 1);
        Debug.Log("Player set axious level " + m_axiousLevel);

        m_minRadius = m_minRadiusArr[m_axiousLevel - 1];
        m_maxRadius = m_maxRadiusArr[m_axiousLevel - 1];

        m_navMeshAgent = GetComponent<NavMeshAgent>();

        MoveToPlayer();
    }

    public void MoveToPlayer()
    {
        Vector3 dir = m_targetObject.position - transform.position;
        dir.Normalize();

        m_navMeshAgent.destination = m_targetObject.position - dir * m_minRadius;
        m_tergetDirection = dir;
        m_currentDirection = transform.forward;
        m_navMeshAgent.speed = 1.0f;
    }


    public void MoveOutPlayer()
    {
        Vector3 newDest = m_targetObject.position - m_currentDirection * Random.Range(m_minRadius, m_maxRadius);
        newDest.y = 0.0f;
        m_navMeshAgent.destination = newDest;
        m_navMeshAgent.speed = 0.8f;
    }

    private void Update()
    {
        if (m_navMeshAgent == null)
            Debug.Log("Nav Mesh Agent is empty");
        if (m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
        {
            //Debug.Log("Nav Mesh Agent Stop.");
            Vector3 noiseTarget = m_targetObject.position + Random.Range(-1.0f, 1.0f) * (new Vector3(Random.Range(m_minRadius, m_maxRadius), 0.0f, Random.Range(m_minRadius, m_maxRadius)));
            m_navMeshAgent.destination = noiseTarget;

            m_tergetDirection = noiseTarget - transform.position;
            m_tergetDirection.Normalize();
            m_currentDirection = transform.forward;
        }

        Debug.DrawRay(transform.position, m_currentDirection, Color.red);

        m_currentDirection = Vector3.RotateTowards(m_currentDirection, m_tergetDirection, m_rotationSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(m_currentDirection);
    }
}
