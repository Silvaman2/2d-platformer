using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDashAfterImage
{
    private PlayerController player;
    private Queue<DashAfterImage> afterImageQueue = new Queue<DashAfterImage>();
    private CountdownTimer afterImageCooldown;

    public PlayerDashAfterImage(PlayerController player)
    {
        this.player = player;
        ResetCooldownTimer();
        InstantiateImages();
    }

    public void SummonImage()
    {
        if (!afterImageCooldown.hasPassed()) return;
        ResetCooldownTimer();
        DashAfterImage currentImage = afterImageQueue.Dequeue();
        currentImage.Summon(player.transform.position, player.transform.rotation);
        afterImageQueue.Enqueue(currentImage);
    }

    private void InstantiateImages()
    {
        for (int i = (int) player.imagesPerDash; i > 0; i--)
        {
            DashAfterImage currentImage = Object.Instantiate(player.dashAfterImageObject);

            InitializeImage(currentImage, i);

            afterImageQueue.Enqueue(currentImage);
        }
    }

    private void ResetCooldownTimer()
    {
        afterImageCooldown = new CountdownTimer(GetTimeBetweenImages());
    }

    private float GetTimeBetweenImages()
    {
        return player.dashDuration / player.imagesPerDash;
    }

    private void InitializeImage(DashAfterImage currentImage, float imageTransparency)
    {
        SpriteRenderer imageRenderer = currentImage.GetComponent<SpriteRenderer>();
        imageRenderer.sprite = player.spriteRenderer.sprite;

        float currentImageTransparency = (255 / player.imagesPerDash) * imageTransparency;

        InitializeColor(currentImage, imageRenderer);
        SetTransparency(currentImage, imageRenderer, currentImageTransparency);
    }

    private void InitializeColor(DashAfterImage currentImage, SpriteRenderer imageRenderer)
    {
        imageRenderer.color = currentImage.dashColor;
    }

    private void SetTransparency(DashAfterImage currentImage, SpriteRenderer imageRenderer, float alpha)
    {
        Color currentColor = imageRenderer.color;
        Debug.Log(alpha);
        imageRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
    }
}
