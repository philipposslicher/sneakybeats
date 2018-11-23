using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float stopDistance;
    public float seekDistance;
    public float startWaitTime;
    public float speed;
    public Transform moveSpot;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    private Rigidbody2D rb;
    private Transform player;
    private Vector3 heading;
    private bool stopCondition1;
    private bool stopCondition2;
    private float waitTime;
    private bool isAlreadyMopping;

    void Start()
    {
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        heading = (player.position - transform.position);
        stopCondition1 = heading.sqrMagnitude > stopDistance * stopDistance;
        stopCondition2 = heading.sqrMagnitude < seekDistance * seekDistance;
        if (stopCondition2)
        {
            if (stopCondition1)
            {
                isAlreadyMopping = false;
                rb.AddForce(heading * speed * Time.deltaTime, ForceMode2D.Force);
            }
            else
            {
                if (!isAlreadyMopping)
                {
                    Debug.Log("Mopping...");
                    isAlreadyMopping = true;
                }
                
            }
        }
        else
        {
            // Patrol();
        }
    }

    private void Patrol()
    {
        Vector3 towardsSpot = moveSpot.position - transform.position;
        rb.AddForce(towardsSpot * speed * Time.deltaTime, ForceMode2D.Force);
        if (towardsSpot.sqrMagnitude < 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
