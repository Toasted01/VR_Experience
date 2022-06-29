using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 8.0f;

    public GameObject player;
    Transform playerTarget;
    Transform doorTarget;
    Transform targetActual;
    NavMeshAgent agent;
    Animator animator;
    bool leaving = false;
    bool walking = true;
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = player.transform;
        doorTarget = door.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(playerTarget.position, transform.position);
        float doorDistance = Vector3.Distance(playerTarget.position, transform.position);
        if (playerDistance <= lookRadius && !leaving)
        {
            lookTarget(1);           
        }
        if (leaving)
        {
            lookTarget(2);
            agent.SetDestination(doorTarget.position);
            if (walking)
            {
                StartCoroutine(WalkAnim());
            }            
        }
    }

    void lookTarget(int num)
    {
        if (num == 1)
        {
            targetActual = playerTarget;
        }
        if (num == 2)
        {
            targetActual = doorTarget;
        }

        Vector3 direction = (targetActual.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        if (gameObject.tag == "Boss3")
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2.5f);
        }
        else 
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
        }
    }

    IEnumerator WalkAnim()
    {
        walking = false;

        animator.SetTrigger("WalkAnim");

        yield return new WaitForSeconds(2);

        walking = true;
    }

    public void leave()
    {
        leaving = true;
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
