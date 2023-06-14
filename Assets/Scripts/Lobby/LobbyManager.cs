using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// �κ� �޴� ��� ���� �� ��ƼŬ ���� �Ŵ��� (�̱���)
public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;
    
    void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

    // ���� ����
    public void _EnterMainMap()
    {
        SceneManager.LoadScene(1);
    }

    // ���� ����
    public void _Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
