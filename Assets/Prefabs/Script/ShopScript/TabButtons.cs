using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButtons : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;

    public Outline outline;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    private void Awake()
    {

        outline = GetComponent<Outline>();
        tabGroup.Subscribe(this);
    }

    private void Start()
    {
        outline.effectColor = new Color32(0, 0, 0, 80);
        outline.effectDistance = new Vector2(-0.76f, 2.64f);
    }


}
