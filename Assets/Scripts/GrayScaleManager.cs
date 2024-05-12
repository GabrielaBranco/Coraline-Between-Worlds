using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GrayScaleManager : MonoBehaviour
{

    [SerializeField] private GameObject[] objectsToGrayScale;
    [SerializeField] private Material grayScaleMaterial;

    private List<SpriteRenderer> grayScaleSprites;
    private List<TilemapRenderer> grayScaleTiles;

    // Start is called before the first frame update
    void Start()
    {
        grayScaleSprites = new List<SpriteRenderer>();
        grayScaleTiles = new List<TilemapRenderer>();
        
        foreach (GameObject obj in objectsToGrayScale)
        {
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            TilemapRenderer tr = obj.GetComponent<TilemapRenderer>();
            if (sr != null)
            {
                sr.material = grayScaleMaterial;
                grayScaleSprites.Add(sr);
            }
            else if (tr != null)
            {
                tr.material = grayScaleMaterial;
                grayScaleTiles.Add(tr);
            }
        }

        foreach (SpriteRenderer sr in grayScaleSprites)
        {
            sr.material.SetFloat("_GrayscaleAmount",0f);
        }
        foreach (TilemapRenderer tr in grayScaleTiles)
        {
            tr.material.SetFloat("_GrayscaleAmount", 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        { 
            foreach (SpriteRenderer sr in grayScaleSprites)
            {
                sr.material.SetFloat("_GrayscaleAmount", 1.0f);
            }
            foreach(TilemapRenderer tr in grayScaleTiles)
            {
                tr.material.SetFloat("_GrayscaleAmount", 1.0f);
            }
        }
        if (Input.GetKeyUp(KeyCode.Q))
        { 
            foreach (SpriteRenderer sr in grayScaleSprites)
            {
                sr.material.SetFloat("_GrayscaleAmount", 0f);
            }
            foreach(TilemapRenderer tr in grayScaleTiles)
            {
                tr.material.SetFloat("_GrayscaleAmount", 0f);
            }
        }
    }
}