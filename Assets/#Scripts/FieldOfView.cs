using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldOfView : MonoBehaviour
{

    public float offsetAngle;
    public float viewRadius;
    public Transform fowardDirectionHelper;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public int meshResolution;
   
    public List<Transform> visibleTargets = new List<Transform>();
    public MeshFilter viewMeshFilter;
    Mesh viewMesh;
    [Space(10)]
    public bool enableEdgeSolver = false;
    public int edgeIterations=6;
    [Space(10)]
    public Transform playerTransform;
    public bool playerInFovSpotted;
    [Space(10)]
    public spitZombieEnemyAi szAiScript;
    public float hitThresholdToDistinguishDifferentObjectHitByRaycast;


    void Start()
    {       
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        //szAiScript = GetComponentInParent<spitZombieEnemyAi>();
        StartCoroutine("FindTargetsWithDelay", .2f);
    }


    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();

            // if player is spotted, then change the state to CHASING but don't change the state when it is attacking

            /*
            if(playerInFovSpotted==true || szAiScript.spitZombieState!= spitZombieEnemyAi.stateSZ.attacking)
            {
                szAiScript.spitZombieState = spitZombieEnemyAi.stateSZ.followingPlayerUntilInSight;
            }
            */
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        
            Transform target = playerTransform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
        if (Vector3.Angle(fowardDirectionHelper.right, dirToTarget) < viewAngle / 2)
        {
            float dstToTarget = Vector3.Distance(transform.position, target.position);

            if (dstToTarget < viewRadius)
            {

                if (!Physics.Raycast(transform.position, dirToTarget, viewRadius, obstacleMask))
                {
                    visibleTargets.Add(target);
                    playerInFovSpotted = true;
                }
                else
                {
                    playerInFovSpotted = false;
                }
            }
            else
            {
                playerInFovSpotted = false;
            }

        }
        else
        {
            playerInFovSpotted = false;
        }

    }

    void LateUpdate()
    {
        DrawFieldOfView();
    }

    void DrawFieldOfView()
    {
        int stepCount = meshResolution;
        float totalSteps = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        float startingAngle = -1 * viewAngle / 2;
        float calculatingAngle = startingAngle;

        bool newRaycast;
        bool oldRaycast=false;

        float oldHitDistance = 10000f;

        float newAngle=0;
        float oldAngle=0;
        for(int i=0; i<= meshResolution; ++i)
        {
            RaycastHit hit;
            Vector3 directionOfRaycast = DirFromAngle(calculatingAngle,true);
            newRaycast = Physics.Raycast(fowardDirectionHelper.position, directionOfRaycast, out hit, viewRadius, obstacleMask);
            float newHitDistance = hit.distance;

            if (enableEdgeSolver)
            {

                if (i > 0)
                {
                    bool exceededEdgeThreshold = Mathf.Abs(newHitDistance - oldHitDistance) > hitThresholdToDistinguishDifferentObjectHitByRaycast;
                    if (oldRaycast != newRaycast || (oldRaycast && newRaycast && exceededEdgeThreshold==true))
                    {
                        EdgeInfo edge = FindEdge(oldAngle, newAngle, oldRaycast, newRaycast, oldHitDistance);
                        if (edge.pointA != Vector3.zero)
                        {                            
                            viewPoints.Add(edge.pointA);
                        }
                        if (edge.pointB != Vector3.zero)
                        {
                            viewPoints.Add(edge.pointB);
                        }
                    }                    
                }
            }

            if (newRaycast)
            {
                viewPoints.Add(hit.point);               
            }
            else
            {
                viewPoints.Add(fowardDirectionHelper.position + directionOfRaycast * viewRadius);
            }

            oldRaycast = newRaycast;
            oldHitDistance = newHitDistance;

            Debug.DrawLine(fowardDirectionHelper.position , fowardDirectionHelper.position + directionOfRaycast * viewRadius);
            oldAngle = calculatingAngle;

            calculatingAngle += totalSteps;
            newAngle = calculatingAngle;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        //Debug.Log((vertexCount - 2) * 3);
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = new Vector3(0,fowardDirectionHelper.localPosition.y,0);
        Debug.DrawLine(transform.position, fowardDirectionHelper.position, Color.green);
        //Debug.Log(Vector3.Magnitude(fowardDirectionHelper.position - transform.position));                  
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();

        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();

    }

    EdgeInfo FindEdge(float minAngle, float maxAngle,bool oldHit,bool newHit,float oldHitDistance)
    {

        Vector3 minPoint=Vector3.zero;
        Vector3 maxPoint=Vector3.zero;

        for (int i=0; i< edgeIterations; ++i)
        {
            RaycastHit hit;
            float midAngle = (minAngle + maxAngle) / 2;
            Vector3 directionOfRaycast = DirFromAngle(midAngle, true);

            bool hitNewBool = Physics.Raycast(fowardDirectionHelper.position, directionOfRaycast, out hit, viewRadius, obstacleMask);
            float newHitDistance = hit.distance;

            bool edgeDstThresholdExceeded = Mathf.Abs(oldHitDistance - newHitDistance) > hitThresholdToDistinguishDifferentObjectHitByRaycast;
            if (hitNewBool==oldHit && !edgeDstThresholdExceeded)
            {
                minAngle = midAngle;

                if (hitNewBool == true)
                {
                    minPoint = hit.point;
                }
                else
                {
                    minPoint = fowardDirectionHelper.position + directionOfRaycast * viewRadius;
                }
            }
            else
            {
                maxAngle = midAngle;

                if(hitNewBool==true)
                {
                    maxPoint = hit.point;
                }
                else
                {
                    maxPoint = fowardDirectionHelper.position + directionOfRaycast * viewRadius;
                }


            }


        }

        if(minPoint!=Vector3.zero)
        {
            Debug.DrawLine(fowardDirectionHelper.position, minPoint, Color.black);
        }

        if(maxPoint!=Vector3.zero)
        {
            Debug.DrawLine(fowardDirectionHelper.position, maxPoint, Color.black);
        }

        return new EdgeInfo(minPoint, maxPoint);
    }


    public struct EdgeInfo {
        public Vector3 pointA;
        public Vector3 pointB;


        public EdgeInfo(Vector3 ptA,Vector3 ptB)
        {
            pointA = ptA;
            pointB = ptB;
        }

    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        Quaternion quat = Quaternion.Euler(0, offsetAngle+angleInDegrees, 0);
        return  quat* fowardDirectionHelper.right;
        //return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }
}