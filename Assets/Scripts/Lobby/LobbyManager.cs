using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 로비 메뉴 기능 제어 및 파티클 연출 매니저 (싱글톤)
public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;
    
    void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

    // 게임 시작
    public void _EnterMainMap()
    {
        SceneManager.LoadScene(1);
    }

    // 게임 종료
    public void _Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
