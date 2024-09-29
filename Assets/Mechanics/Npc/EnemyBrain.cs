using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    private GameObject _player;
    private NavMeshAgent _agent;

    public void Init(GameObject player)
    {
        _player = player;
        _agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        _agent.SetDestination(_player.transform.position);
    }
}
