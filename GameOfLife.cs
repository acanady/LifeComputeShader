using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameOfLife : MonoBehaviour
{
    public ComputeShader LifeShader;
    public Material material;
    public Texture Input;
    public int Width;
    public int Height;

    private int kernel;

    RenderTexture renderTexture;

    private void Start()
    {
        
    }

    private void Update()
    {
        Life();
    }

    private void Life()
    {
        kernel = LifeShader.FindKernel("CSMain");
        renderTexture = new RenderTexture(Width, Height, 24);
        renderTexture.enableRandomWrite = true;
        renderTexture.wrapMode = TextureWrapMode.Repeat;
        renderTexture.filterMode = FilterMode.Point;
        renderTexture.useMipMap = false;
        renderTexture.Create();
        LifeShader.SetFloat("Width", Width);
        LifeShader.SetFloat("Height", Height);
        LifeShader.SetTexture(kernel, "Input", Input);
        LifeShader.SetTexture(kernel, "Result", renderTexture);
        LifeShader.Dispatch(kernel, Width / 8, Height / 8, 1);

        Input = renderTexture;
        material.mainTexture = Input;
    }
}
