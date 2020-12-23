using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmChanger : MonoBehaviour
{
    [Tooltip("鳴らしたいBGM"), SerializeField]
    TinyAudio.Bgm bgm = TinyAudio.Bgm.Gameover;

    void Start()
    {
        TinyAudio.PlayBGM(bgm);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
