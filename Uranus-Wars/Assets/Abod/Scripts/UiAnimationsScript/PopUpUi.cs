using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpUi : MonoBehaviour
{
    [SerializeField] GameObject BuildingPanel;
    [SerializeField] GameObject closeBtn;
    [SerializeField] GameObject openBtn;
    public void OnClickOpenButton()
    {
        LeanTween.moveLocalX(BuildingPanel, -646f, 1f);
        openBtn.SetActive(false);
        closeBtn.SetActive(true);
    }
    public void OnClickCloseButton()
    {
        LeanTween.moveLocalX(BuildingPanel, -1180f, 1f);
        openBtn.SetActive(true);
        closeBtn.SetActive(false);
    }
}
