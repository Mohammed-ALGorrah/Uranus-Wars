using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenPanelsScript : MonoBehaviour
{
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject findGamePanel;

    public void OnClickShopButton()
    {
        shopPanel.SetActive(true);
    }
    public void OnClickStoreButton()
    {
        storePanel.SetActive(true);
    }
    public void OnClickSettingButton()
    {
        settingPanel.SetActive(true);
    }
    public void OnClickFindGameButton()
    {
        findGamePanel.SetActive(true);
        SceneManager.LoadScene(2);
    }

}
