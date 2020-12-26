using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject particlePrefab = default;
    float cameraDistance = 0;
    Rigidbody rb = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        cameraDistance = Vector3.Distance(Camera.main.transform.position,
            transform.position);
    }

    // Update is called once per frame
    void Update()
    {
      // 左に移動
        if (Input.GetKey (KeyCode.LeftArrow)) {
            this.transform.Translate (-0.1f,0.0f,0.0f);
        }
        // 右に移動
        if (Input.GetKey (KeyCode.RightArrow)) {
            this.transform.Translate (0.1f,0.0f,0.0f);
        }
        // 前に移動
        if (Input.GetKey (KeyCode.UpArrow)) {
            this.transform.Translate(0.0f, 0.1f, 0.0f);
        }
        // 後ろに移動
        if (Input.GetKey (KeyCode.DownArrow)) {
            this.transform.Translate(0.0f, -0.1f,0.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit" + collision.collider.name + "" + collision.contacts[0].point);
        if (collision.collider.CompareTag("Enemy"))
        {
            // TODO: 得点処理
            TinyAudio.PlaySe(TinyAudio.Se.Get);
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameManager.ToGameover();

        }

    }

}
