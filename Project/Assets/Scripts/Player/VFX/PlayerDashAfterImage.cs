using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDashAfterImage
{
    private PlayerController player;
    private Queue<DashAfterImage> afterImageQueue = new Queue<DashAfterImage>();
    private float currentImageTransparency;
    private int currentSortingOrder;

    public PlayerDashAfterImage(PlayerController player)
    {
        this.player = player;
        InstantiateImages();
    }

    public void SummonImage()
    {
        DashAfterImage currentImage = afterImageQueue.Dequeue();
        currentImage.Summon(player.transform.position, player.transform.rotation);

        AdjustAfterImageEffect(currentImage);

        afterImageQueue.Enqueue(currentImage);
    }

    private void InstantiateImages()
    {
        for (int i = 0; i < 20; i++)
        {
            DashAfterImage currentImage = Object.Instantiate(player.dashAfterImageObject);

            InitializeColor(currentImage);

            afterImageQueue.Enqueue(currentImage);
        }
    }

    private void InitializeColor(DashAfterImage currentImage)
    {
        currentImage.spriteRenderer.color = currentImage.dashColor;
    }

    private void SetTransparency(DashAfterImage currentImage)
    {
        Color currentColor = currentImage.spriteRenderer.color;
        currentImage.spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentImageTransparency);

        IncreaseImageTransparency();
    }

    public void ResetAfterImageEffect()
    { 
        ResetImageTransparency();
        ResetSortingOrder();
    }

    public void AdjustAfterImageEffect(DashAfterImage currentImage)
    {
        SetTransparency(currentImage);
        SetSortingOrder(currentImage);
        SetSprite(currentImage);
        SetFacing(currentImage);
    }

    public void ResetImageTransparency()
    {
        currentImageTransparency = 0f;
    }

    private void IncreaseImageTransparency()
    {
        currentImageTransparency += 1f / 13f;
        currentImageTransparency = Mathf.Min(currentImageTransparency, 0.95f);
        Debug.Log(currentImageTransparency);
    }

    public void ResetSortingOrder()
    {
        currentSortingOrder = 0;
    }

    public void SetSortingOrder(DashAfterImage currentImage)
    {
        currentImage.spriteRenderer.sortingOrder = currentSortingOrder++;
    }

    private void SetSprite(DashAfterImage currentImage)
    {
        currentImage.spriteRenderer.sprite = player.spriteRenderer.sprite;
    }
    
    private void SetFacing(DashAfterImage currentImage)
    {
        currentImage.spriteRenderer.flipX = player.spriteRenderer.flipX;
    }
}
