using UnityEngine;

public class ChaseState : IEnemyState
{
    private StatePatternEnemy enemy;


    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Chase();
        Look();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ToAlertState();
        }
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {

    }

    public void ToPatrolState()
    {

    }

    void Look()
    {

        Vector3 enemyToTarget = enemy.chaseTarget.position - enemy.eye.position;
        // Let's visualize the eye ray
        Debug.DrawRay(enemy.eye.position, enemyToTarget, Color.red);
        // Raycasting from the eye
        RaycastHit hit;
        if (Physics.Raycast(enemy.eye.position, enemyToTarget, out hit, enemy.sightRange) &&
            hit.transform.CompareTag("Player"))
        {
            // ENemy makes sure it sees the target. 
            enemy.chaseTarget = hit.transform;

        }
        else
        {
            // Enemy does not see player anymore. Go to Alert state
            ToAlertState();
        }

    }



    void Chase()
    {

        enemy.indicator.material.color = Color.red;
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.isStopped = false;

    }

}