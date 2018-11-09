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
    private bool stopCondition;
    private float waitTime;

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
        stopCondition = heading.sqrMagnitude > stopDistance * stopDistance && heading.sqrMagnitude < seekDistance * seekDistance;
        if (stopCondition)
        {
            rb.AddForce(heading * speed * Time.deltaTime, ForceMode2D.Force);
        }
        else
        {
            Patrol();
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
