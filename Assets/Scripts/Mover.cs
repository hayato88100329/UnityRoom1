using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    float speedMin = 1f;
    [SerializeField]
    float speedMax = 5f;

    float speed;
    Rigidbody rb = default;

    private void Awake()
    {
        //speedMin～、speedMaxの速度を乱数で求める
        speed = Random.Range(speedMin, speedMax);
        //ローカル変数thに、0～359の角度を乱数で求める
        var th = Random.Range(0, 360);
        //ローカル変数dirに、角度thで長さ1の方向ベクトルを求める
        var dir = new Vector3(Mathf.Cos(th*Mathf.Deg2Rad), Mathf.Sin(th*Mathf.Deg2Rad),0);
        //変数rbに、Rigidbodyのインスタンスを取得する
        rb = GetComponent<Rigidbody>();
        //以上で求めた値を使って、速度を設定する
        rb.velocity = dir * speed;

    }
    private void FixedUpdate()
    {
        if(Mathf.Approximately(rb.velocity.magnitude,0f))
        {

        }
    }
}
