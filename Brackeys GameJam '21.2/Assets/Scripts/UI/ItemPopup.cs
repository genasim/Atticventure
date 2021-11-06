using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPopup : MonoBehaviour
{
    Rect rect;
    public Texture texture;
    float itemDescriptionTime = 2f;

    void Start()
    {
        float size = Screen.width * 0.1f;
        rect = new Rect(Screen.width / 2 - size / 2, Screen.height * 0.3f, size, size);
    }

    void Update()
    {
        if(itemDescriptionTime > 0)
            itemDescriptionTime -= Time.deltaTime;
    }

    private void OnGUI()
    {
        if (itemDescriptionTime > 0)
            GUI.DrawTexture(rect, texture);
    }
}
