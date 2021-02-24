﻿using UnityEngine;
using System.Collections;

public class TextureScroll : MonoBehaviour {

    public float scrollSpeed = 0.5F;
    Renderer rend;
    public bool ScrollXRight;
    public bool ScrollXLeft;
    public bool ScrollYUp;
    public bool ScrollYDown;
    public bool ScrollXYPositive;
    public bool ScrollXYNegative;

    public bool Skinned;

    // Update is called once per frame
    private void Start()
    {
        if (Skinned)
        {
            rend = GetComponent<SkinnedMeshRenderer>();
        }
        else
            rend = GetComponent<MeshRenderer>();
    }
    void Update () {
        float offset = Time.time * scrollSpeed;
     
        if (ScrollXRight)
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
        else if (ScrollYUp)
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        }
        else if (ScrollXYPositive)
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, offset));
        }
        else if (ScrollXLeft)
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));
        }
        else if (ScrollYDown)
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(0, -offset));
        }
        else
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(-offset, -offset));
        }
    }
}
