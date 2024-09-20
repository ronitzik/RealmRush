using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField][Range(0f, 5f)] float speed = 1.2f;
    Enemy enemy;


    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void FindPath()
    {
        path.Clear();

        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");

        // Sort the waypoints based on their X coordinate for left-to-right movement
        path = waypoints.Select(waypoint => waypoint.GetComponent<Waypoint>())
                        .Where(waypoint => waypoint != null)
                        .OrderBy(waypoint => waypoint.transform.position.x)
                        .ThenBy(waypoint => waypoint.transform.position.z)
                        .ToList();
    }


    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }
    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }


    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPos);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();

            }
        }
        enemy.StealGold();
        gameObject.SetActive(false);
        FinishPath();
    }
}

