using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyBlockTest : Interactable
{
    public Color swapToColor;

    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;

    public override void Interact(GameObject player)
    {
        ChangeColor();
    }

    private void Start()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
    }

    private void ChangeColor()
    {
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetColor("_EmissionColor", swapToColor);
        //_propBlock.SetColor("_BaseColor", swapToColor);
        _renderer.SetPropertyBlock(_propBlock);

    }
}
