using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundCheker : MonoBehaviour
{
    Camera cam = null;
    SphereCollider sphereCollider = null;
    Rigidbody rb = null;
    private bool chrXDot;


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
        var rightBottom = cam.ViewportToWorldPoint(
            new Vector3(1, 0, cameraDistance));

        var chrMoveVector = rightBottom - leftBottom;
        var horizontal = chrMoveVector.normalized;

        var chrXVector = transform.position - leftBottom;
        var chrXDot = Vector3.Dot(chrXVector, horizontal);
        if (chrXDot < sphereCollider.radius)
        {
            var v = rb.velocity;
            v.x = Mathf.Abs(v.x);
            rb.velocity = v;
        }
        else if (chrXDot > chrMoveVector.magnitude - sphereCollider.radius)
        {
            var v = rb.velocity;
            v.x = -Mathf.Abs(v.x);
            rb.velocity = v;
        }
        //ここまでが左から右反射








        var leftTop = cam.ViewportToWorldPoint(
            new Vector3(0, 1, cameraDistance));
        var bottomToTop = leftTop - leftBottom;
        var vertical = bottomToTop.normalized;
        var chrBottom = transform.position - sphereCollider.radius * vertical;
        var chrBottomVector = chrBottom - leftBottom;
        var chrBottomDot = Vector3.Dot(chrBottomVector, vertical);

        Debug.Log($"{chrMoveVector.magnitude}");

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
