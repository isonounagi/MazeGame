using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    private AudioSource audioSource;
    //　鳴らす音声クリップ
    [SerializeField]
    private AudioClip appearSE;
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(appearSE, 0.3f);
            SoundStop();
        }
    }

    void SoundStop() //音を一回だけ鳴らすようにするのでコライダを消している
    {
        SphereCollider component = this.gameObject.GetComponent<SphereCollider>();
        // 指定したコンポーネントを削除
        Destroy(component);
    }

    private void LateUpdate()
    {
        
    }

}
