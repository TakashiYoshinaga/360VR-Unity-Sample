using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class MainCoordinator : MonoBehaviour
{
    [SerializeField]
    SceneChanger _SceneChanger;
    [SerializeField]
    MeshRenderer _FadeObjectRenderer;
    [SerializeField]
    Interactable _Scene1Button;
    [SerializeField]
    Interactable _Scene2Button;
    [SerializeField]
    Interactable _HomeButton;
    [SerializeField]
    Transform _SceneRoot;
    [SerializeField]
    GameObject _HomeScene;
    [SerializeField]
    GameObject _Scene1;
    [SerializeField]
    GameObject _Scene2;
    [SerializeField]
    float _SceneChangeDuration = 2f;

    GameObject _CurrentScene;
    // Start is called before the first frame update
    void Start()
    {
        //シーン切り替え用のオブジェクトを準備
        _SceneChanger.Prepare(_FadeObjectRenderer, _SceneChangeDuration);
        //Scene1ボタンが押されたらScene1に移動
        _Scene1Button.OnClick.AddListener(() =>
        {
            _SceneChanger.SceneChange(_HomeScene, _Scene1);
            _CurrentScene = _Scene1;
            StartCoroutine(VisibleHomeButtonWithDelay(_SceneChanger.TotalFadeDuraton));
        });
        //Scene2ボタンが押されたらScene2に移動
        _Scene2Button.OnClick.AddListener(() =>
        {
            _SceneChanger.SceneChange(_HomeScene, _Scene2);
            _CurrentScene = _Scene2;
            StartCoroutine(VisibleHomeButtonWithDelay(_SceneChanger.TotalFadeDuraton));
        });
        //Homeボタンが押されたらHomeSceneに移動
        _HomeButton.OnClick.AddListener(() =>
        {
            _SceneChanger.SceneChange(_CurrentScene, _HomeScene);
            _HomeButton.gameObject.SetActive(false);
        });
        //最初はHomeButtonを非表示
       _HomeButton.gameObject.SetActive(false);

#if UNITY_EDITOR
        //エディタ上でのみ、シーンの高さ方向を調整。Quest実機では不要
        _SceneRoot.transform.position = new Vector3(0, -1.6f, 0);
#endif

    }
    //一定時間後にHomeButtonを表示する
    IEnumerator VisibleHomeButtonWithDelay(float delay){
        yield return new WaitForSeconds(delay);
        _HomeButton.gameObject.SetActive(true);
    }
   
}
