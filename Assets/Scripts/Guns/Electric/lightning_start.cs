using UnityEngine;
using System.Collections;
 
/// <summary>
/// NO SHAME -- I TOOK THIS FROM THE INTERWEBS BUT I UNDERSTAND HOW IT WORKS
/// Uses a line renderer and random vector creation to make it look like lightning
/// </summary>

public class lightning_start : MonoBehaviour
{
    public GameObject target;
    public Vector3 targetOriginPosition;
    private LineRenderer lineRend;
    public Material lineMat;

    // Variables for lineRenderer
    //
    private float arcLength = 1.0f;
    private float arcVariation = 1.0f;
    private float inaccuracy = 0.5f;
    private float timeOfZap = .2f;
    private float zapTimer;

    void Start()
    {
        // Adds a lineRenderer component and stores a reference, sets inital length of zap to 0 so it doesn't zap immediately
        //
        lineRend = gameObject.AddComponent<LineRenderer>();
        zapTimer = 0;
        lineRend.positionCount = 1;

        // Grabs the bullet material and applies it to the lineRenderer
        //
        GameObject bullet = Resources.Load("bullet") as GameObject;
        lineMat = bullet.GetComponent<Renderer>().sharedMaterial;
        lineRend.material = lineMat;
    }

    void Update()
    {
        // Main code that does the zapping.
        //
        if (zapTimer > 0)
        {
            lineRend.startWidth = .1f;
            lineRend.endWidth = .1f;

            Vector3 lastPoint = transform.position;
            int i = 1;
            lineRend.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z + .1f));   //make the origin of the LR the same as the transform

            // Constructs a collection of randomized line segments
            while (Vector3.Distance(target.transform.position, lastPoint) > 3.0f)                                           // Checks to see if the last point is still far away from the target
            {   
                lineRend.positionCount = i + 1;                              // Add a new vertex in our line renderer
                Vector3 fwd = target.transform.position - lastPoint;        // gives the direction to our target from the end of the last arc
                fwd.Normalize();                                            // makes the direction to scale
                fwd = Randomize(fwd, inaccuracy);                           // we don't want a straight line to the target though
                fwd *= Random.Range(arcLength * arcVariation, arcLength);   // nature is never too uniform
                fwd += lastPoint;                                           //point + distance * direction = new point. this is where our new arc ends
                lineRend.SetPosition(i, fwd);                               //this tells the line renderer where to draw to
                i++;
                lastPoint = fwd;                                            //so we know where we are starting from for the next arc
            }
            lineRend.positionCount = i + 1;
            lineRend.SetPosition(i, target.transform.position);
            zapTimer -= Time.deltaTime;
        }
        else
            lineRend.positionCount = 1;
    }

    // Given an input vector, it outputs a vector with randomized points.
    // the deviation input (float) controls how erratic the new vector is
    // normalizes it so they're all the same size
    //
    private Vector3 Randomize(Vector3 newVector, float deviation)
    {
        newVector += new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * deviation;
        newVector.Normalize();
        return newVector;
    }

    // public facing function to initiate zapping of stuff
    //
    public void ZapTarget(GameObject newTarget)
    {
        target = newTarget;
        zapTimer = timeOfZap;
    }
}
