using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Vector3 GetTop() {
        return transform.parent.InverseTransformPoint(transform.TransformPoint(new Vector3(0, 0.5f, 0)));
    }

    public Vector3 GetBottom() {
        return transform.parent.InverseTransformPoint(transform.TransformPoint(new Vector3(0, -0.5f, 0)));
    }

    public Vector3 GetPosition() {
        return transform.localPosition;
    }
}
