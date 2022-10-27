    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InfoPopup : Subs
{
    //este script se lo adjuntas a un Interactable que quieras que muestre un mensaje en pantalla mientras lo miras

    protected Interactable yo;
    public MouseLook mouseLook;

    [SerializeField]
    private Color infoColor;


    void Start()
    {
        if (GetComponent<Interactable>() != null)
        {
            yo = GetComponent<Interactable>();
        }

        if (mouseLook == null)
        {
            mouseLook = FindObjectOfType<MouseLook>();
        }
    }

    public void StartShow()
    {
        Show(desiredText, desiredTime);
    }

    public override void Show(string text, float time)
    {
        subsCanvasText.text = text;
        subsCanvasText.fontStyle = FontStyle.Normal;
        subsCanvasText.color = infoColor;
        Invoke("Hide", time);
    }

    public override void Hide()
    {
        subsCanvasText.text = "";
    }

    public void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) 
        {
            return;
        }
        Hide();
    }
}
