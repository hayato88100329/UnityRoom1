using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScreenLimitter : MonoBehaviour
{
    Camera cam = null;
    SphereCollider sphereCollider = null;
    Rigidbody rb = null;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        sphereCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        var cameraToChr = transform.position - cam.transform.position;
        var cameraDistance = Vector3.Dot(cameraToChr, cam.transform.forward);

        var leftBottom = cam.ViewportToWorldPoint(
            new Vector3(0, 0, cameraDistance));
        var rightBotton = cam.ViewportToWorldPoint(
            new Vector3(1, 0, cameraDistance));
        var chrMoveVector = rightBotton - leftBottom;

        Debug.Log($"{chrMoveVector.magnitude}");

        var horizontal = chrMoveVector.normalized;//normalizedは長さを1にする
        var chrLeft = transform.position - sphereCollider.radius * horizontal;
        var chrLeftVector = chrLeft - leftBottom;
        var chrLeftDot = Vector3.Dot(chrLeftVector, horizontal);
        Debug.Log($"{chrLeftDot} {chrMoveVector.magnitude}");

        if (chrLeftDot < 0)
        {
            var pos = transform.position;
            pos -= chrLeftDot * horizontal;
            transform.position = pos;


        }
        else
        {
            var chrRight = transform.position + sphereCollider.radius * horizontal;
            var chrRightVector = chrRight - leftBottom;
            var chrRightDot = Vector3.Dot(chrRightVector, horizontal);
            if (chrRightDot > chrMoveVector.magnitude)
            {
                var pos = transform.position;
                pos -= (chrRightDot - chrMoveVector.magnitude) * horizontal;
                transform.position = pos;
            }
        }

        var leftTop = cam.ViewportToWorldPoint(
            new Vector3(0, 1, cameraDistance));
        var bottomToTop = leftTop - leftBottom;
        var vertical = bottomToTop.normalized;
        var chrBottom = transform.position - sphereCollider.radius * vertical;
        var chrBottomVector = chrBottom - leftBottom;
        var chrBottomDot = Vector3.Dot(chrBottomVector, vertical);
        if (chrBottomDot < 0)
        {
            var pos = transform.position;
            pos -= chrBottomDot * vertical;
            transform.position = pos;
        }
        else
        {
            var chrTop = transform.position + sphereCollider.radius * vertical;
            var chrTopVector = chrTop - leftBottom;
            var chrTopDot = Vector3.Dot(chrTopVector, vertical);
            if (chrTopDot > bottomToTop.magnitude)
            {
                var pos = transform.position;
                pos -= (chrTopDot - bottomToTop.magnitude) * vertical;
                transform.position = pos;

            }
        }




    }
}
