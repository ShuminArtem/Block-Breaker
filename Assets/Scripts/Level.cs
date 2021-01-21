using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int breakebleBlocks;

    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }
    public void CountBlocks()
    {
        breakebleBlocks++;
    }

    public void BlockDestroyed()
    {
        breakebleBlocks--;
        if(breakebleBlocks <=0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
