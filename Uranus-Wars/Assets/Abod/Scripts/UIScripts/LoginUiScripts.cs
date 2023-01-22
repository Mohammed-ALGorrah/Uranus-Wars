using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginUiScripts : MonoBehaviour
{
    [SerializeField] GameObject registerPanel;
    public void OnClickLoginButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClicGoToRegisterButton()
    {
        this.gameObject.SetActive(false);
        registerPanel.SetActive(true);
    }
}
