using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToNextScene : MonoBehaviour
{
    [Tooltip("切り替えたいシーン名"), SerializeField]
    string nextScene = "";

    [Tooltip("シーン切り替え直後に次のシーンに切り替えないようにするにはチェック")]
    public bool sceneChanged = false;

    public void SetChangeFalse()
    {
        sceneChanged = false;
    }


    // Update is called once per frame
    void Update()
    {
        //sceneChangedがtrueだったら、Updateはすぐ終了
        if (sceneChanged) return;

        var rank = SceneManager.GetSceneByName("Ranking");
        if (rank.IsValid()) return;
        //if (rank.IsValid() || !GameManager.CanChangeToTitle) return;

        if (Input.GetButtonDown("Click"))
        {
            sceneChanged = true;
            //TinyAudio.PlaySe(TinyAudio.Se.Click);
            SceneManager.LoadScene(nextScene);
        }


    }

}
