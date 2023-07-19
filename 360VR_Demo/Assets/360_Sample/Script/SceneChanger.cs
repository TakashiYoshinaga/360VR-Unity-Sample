using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    //フェードイン/フェードアウトに使うオブジェクトのレンダラー
    MeshRenderer _FadeRenderer;
  
    //フェードイン/フェードアウトそれぞれにかかる時間
    float _HalfFadeDuration = 2f;
    float _WaitSeconds = 0.5f;
    //フェードイン/フェードアウト全体でかかる時間
    public float TotalFadeDuraton
    {
        get { return _HalfFadeDuration*2+_WaitSeconds; }
    }
    //初期設定
    public void Prepare(MeshRenderer fadeObjectRenderer, float halfFadeDuration)
    {
        _FadeRenderer = fadeObjectRenderer;
        _HalfFadeDuration = halfFadeDuration;
    }
    //シーンを移動する
    public void SceneChange(GameObject current, GameObject next)
    {
        StartCoroutine(SceneChangeCoroutine(current, next));
    }
    //シーンを移動するする際にフェードアウト/フェードインを行う
    IEnumerator SceneChangeCoroutine(GameObject current, GameObject next)
    {
        //フェード用のオブジェクトを表示
        _FadeRenderer.enabled = true;
        //フェードイン/フェードアウトに使うマテリアルを取得。このあと透過度を変えてフェードを実現する
        Material _BlackMaterial=_FadeRenderer.material;
        //フェードアウト開始
        _BlackMaterial.color = new Color(0, 0, 0, 0);
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / _HalfFadeDuration;
            _BlackMaterial.color = new Color(0, 0, 0, t);
            yield return null;
        }
        //全体が暗くなったらシーンを切り替える
        current.SetActive(false);
        next.SetActive(true);
        //ちょっとだけ真っ暗な状態で待つ
        yield return new WaitForSeconds(_WaitSeconds);
        //フェードイン開始
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / _HalfFadeDuration;
            _BlackMaterial.color = new Color(0, 0, 0, 1 - t);
            yield return null;
        }
        //最後にフェード用のオブジェクトを非表示
        _FadeRenderer.enabled = false;
    }
}
