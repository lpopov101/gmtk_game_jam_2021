using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBehavior : MonoBehaviour {
    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    private float ropeSegLen = 0.25f;
    private int segmentLength = 20;
    private float lineWidth = 0.1f;

    public Transform StartPoint;
    public Transform EndPoint;

    // Start is called before the first frame update
    void Start() {
        // GameObject cube2 = FindGameObjectWithTag("Test2");
        // FixedJoint2D joint = cube2.GetComponent(typeof(FixedJoint2D));
        // joint.enabled = false;


        this.lineRenderer = this.GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < segmentLength; i++)
        {
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    // Update is called once per frame
    void Update() {
        float dist = checkDistance();
        if (dist >= 8.0f) {
            GameObject cube1 = GameObject.FindGameObjectWithTag("Test1");
            FixedJoint2D joint = cube1.GetComponent(typeof(FixedJoint2D)) as FixedJoint2D;
            joint.enabled = true;
        }
        this.DrawRope();
    }

    float checkDistance() {
        float pos1x = GameObject.FindGameObjectWithTag("Test1").transform.position.x;
        float pos1y = GameObject.FindGameObjectWithTag("Test1").transform.position.y;
        float pos2x = GameObject.FindGameObjectWithTag("Test2").transform.position.x;
        float pos2y = GameObject.FindGameObjectWithTag("Test2").transform.position.y;

        float xDiff = pos1x - pos2x;
        float yDiff = pos1y - pos2y;

        float xDiffSq = xDiff * xDiff;
        float yDiffSq = yDiff * yDiff;

        float distance = Mathf.Sqrt(xDiffSq + yDiffSq);

        return distance;
    }

    private void FixedUpdate() {
        this.Simulate();
    }

    private void Simulate() {

        Vector2 forceGravity = new Vector2(0f, -1.5f);

        for (int i = 1; i < this.segmentLength; i++)
        {
            RopeSegment firstSegment = this.ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            this.ropeSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++) {
            this.ApplyConstraint();
        }
    }

    private void ApplyConstraint() {

        //Constrant to Mouse
        // RopeSegment firstSegment = this.ropeSegments[0];
        // firstSegment.posNow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // this.ropeSegments[0] = firstSegment;

        //Constrant to First Point 
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = GameObject.FindGameObjectWithTag("Test1").transform.position;
        this.ropeSegments[0] = firstSegment;


        //Constrant to Second Point 
        RopeSegment endSegment = this.ropeSegments[this.ropeSegments.Count - 1];
        endSegment.posNow = GameObject.FindGameObjectWithTag("Test2").transform.position;
        this.ropeSegments[this.ropeSegments.Count - 1] = endSegment;

        for (int i = 0; i < this.segmentLength - 1; i++) {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegLen) {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < ropeSegLen) {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0) {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            }
            else {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }
        }
    }

    private void DrawRope() {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[this.segmentLength];
        for (int i = 0; i < this.segmentLength; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}
