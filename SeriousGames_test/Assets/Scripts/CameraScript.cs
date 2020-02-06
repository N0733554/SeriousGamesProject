using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Vector3 Origin;
    public float CamMoveSpeed;

    void Start()
    {
        Origin = transform.position;
    }
    IEnumerator LerpToPos(Vector3 pos1, Vector3 pos2, float lerpSpeed)
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / lerpSpeed);

            transform.position = Vector3.Lerp(pos1, pos2, t);
            yield return 0;
        }
    }

    public void MoveToOrigin()
    {
        StartCoroutine(LerpToPos(transform.position, Origin, CamMoveSpeed));
    }

    public void MoveToPos(Vector3 pos2)
    {
        StartCoroutine(LerpToPos(transform.position, pos2, CamMoveSpeed));
    }
}
