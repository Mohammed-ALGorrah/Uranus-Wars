using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegisterUIScripts : MonoBehaviour
{
    [SerializeField] GameObject loginPanel;
    public void OnClickRegisterButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClicGoToLoginButton()
    {
        this.gameObject.SetActive(false);
        loginPanel.SetActive(true);
    }
}

