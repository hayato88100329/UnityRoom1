using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    int point = 100;
    [SerializeField]
    GameObject particlePrefab = default;

    static int count = 0;
    private static float time;

    public static void ClearCount()
    {
        count++;
        Debug.Log(count);
    }

    public static float GetTime
    {
        get
        {
            return Mathf.Round(time * 10f) / 10f;
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit" + collision.collider.name + "" + collision.contacts[0].point);
        if (collision.collider.CompareTag("Player"))
        {
            // TODO: 得点処理
            GameManager.Instance.AddPoint((int)(point * GameManager.GetTime));

            GameManager.Instance.AddPoint(point);
            TinyAudio.PlaySe(TinyAudio.Se.Get);
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);

            //TODO:クリアチェック
            count--;
            if (count <= 0)
            {
                GameManager.ToClear();
            }


        }

    }
}
