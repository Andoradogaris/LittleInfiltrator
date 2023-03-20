using UnityEngine;
using System.Collections;

[System.Serializable]
public class PathPoint
{
    public Transform point;
    public float pauseTime;
}

public class Guard : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f; // speed of guard
    [SerializeField]
    private float rotationSpeed = 2f; // speed of rotation towards next waypoint
    [SerializeField]
    private PathPoint[] path;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private bool Fixed;

    private int currentWaypointIndex; // index of current waypoint
    private Transform currentWaypoint; // current waypoint being followed
    private bool isWaiting; // flag to indicate if guard is waiting at a waypoint
    private bool isMovingForward; // flag to indicate if guard is moving forward or backward along path

    private GameObject player;
    private bool isInside;

    // Start is called before the first frame update
    void Start()
    {
        currentWaypointIndex = 0;
        if (!Fixed)
        {
            currentWaypoint = path[currentWaypointIndex].point.transform;
        }
        isWaiting = false;
        isMovingForward = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaiting && !Fixed)
        {
            // calculate direction towards current waypoint
            Vector3 direction = (currentWaypoint.position - transform.position).normalized;
            // calculate angle between current direction and desired direction towards next waypoint
            float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);

            // if angle is greater than 1 degree, rotate towards next waypoint
            if (Mathf.Abs(angle) > 1f)
            {
                transform.Rotate(Vector3.up, angle * rotationSpeed * Time.deltaTime);
            }
            else
            {
                // move guard toward current waypoint
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, speed * Time.deltaTime);

                // check if guard has reached current waypoint
                if (transform.position == currentWaypoint.position)
                {
                    // if guard has reached last waypoint, reset to first waypoint
                    if (currentWaypointIndex == path.Length - 1)
                    {
                        currentWaypointIndex = 0;
                        currentWaypoint = path[currentWaypointIndex].point.transform;
                    }
                    else
                    {
                        // else, move to next waypoint
                        currentWaypointIndex += isMovingForward ? 1 : -1;
                        currentWaypoint = path[currentWaypointIndex].point.transform;
                    }

                    // set flag to indicate guard is waiting
                    isWaiting = true;
                    // start coroutine to wait at current waypoint
                    StartCoroutine(WaitAtWaypoint(path[currentWaypointIndex].pauseTime));
                }
            }
        }
        if (isInside)
        {
            DrawRaycast(player);
            if (!IsObstructed(player))
            {
                StartCoroutine(player.GetComponent<PlayerController>().Die());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;
        }
    }

    private bool IsObstructed(GameObject player)
    {

        int layerMask = ~(1 << LayerMask.NameToLayer("Ignore Raycast"));
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.forward * 0.5f + transform.up * 0.4f, player.transform.position - transform.position, out hit, Vector3.Distance(transform.position, player.transform.position), layerMask))
        {
            if (hit.collider.CompareTag("Player"))
            {
            return false;
            }
            else
            {
            return true;
            }
        }
        return true;
    }


    IEnumerator WaitAtWaypoint(float waitTime)
    {
        anim.SetBool("isWalking", false);
        // wait for specified time
        yield return new WaitForSeconds(waitTime);
        anim.SetBool("isWalking", true);
        // set flag to indicate guard is no longer waiting
        isWaiting = false;

        // if guard has reached last waypoint, reverse direction
        if (currentWaypointIndex == path.Length - 1)
        {
            isMovingForward = false;
        }
        // if guard has reached first waypoint, resume forward direction
        else if (currentWaypointIndex == 0)
        {
            isMovingForward = true;
        }
    }


    private void DrawRaycast(GameObject player)
    {
        Vector3 raycastDirection = player.transform.position - transform.position;

        // Draw the Raycast in the Scene view
        Debug.DrawRay(transform.position + transform.forward * 0.5f + transform.up * 0.4f, raycastDirection, Color.blue);
    }

}
