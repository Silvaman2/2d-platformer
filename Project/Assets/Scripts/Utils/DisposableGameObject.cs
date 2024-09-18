using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DisposableGameObject : MonoBehaviour
{
    /*
    This class is for game objects that are going to be instantiated only once, and then re-used throughout the game. 
    This is because constantly instantiating and destroying objects may hinder performance. Bullets is the best example of this.
    The SummonObject method is meant to replace the Instantiate method, as it will instead set the object to active.
    The HideObject method is meant to replace the Destroy method, as it will hide the object, instead of destroying it.

    Ultimately, override the start method when you want to initialize properties that you want to initialize once, 
    and SummonStart, as a method that gets called every summon.
    */


    public void Start()
    {
        Hide();
    }

    /// <summary>
    /// Method <c>SummonStart<c> This is the class' substitute to the Start method. It will be called each time the SummonObject method is called.
    /// </summary>
    protected abstract void SummonStart();

    /// <summary>
    /// Method <c>SummonObject<c> Sets the object's active property to true
    /// </summary>
    public DisposableGameObject Summon(Vector2 position, Quaternion quaternion)
    {
        gameObject.SetActive(true);
        transform.position = position;
        transform.rotation = quaternion;
        SummonStart();
        return this;
    }

    /// <summary>
    /// Method <c>HideObject<c> Sets the object's active property to false
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
