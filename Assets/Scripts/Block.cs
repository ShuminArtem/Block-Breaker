using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config param
    [SerializeField] AudioClip breakeSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    // cached ref
    Level level;

    // stare var
    [SerializeField] int timesHit; // TODO only serialized for debug puposes
    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
        {
            HandleHit();
        }

    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing form array" + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        BlockDestroySFX();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparkles();
    }

    private void BlockDestroySFX()
    {
        FindObjectOfType<GameSession>().AddToscore();
        AudioSource.PlayClipAtPoint(breakeSound, Camera.main.transform.position);
    }

    private void TriggerSparkles()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX,transform.position,transform.rotation);
        Destroy(sparkles, 1f);
    }
}
