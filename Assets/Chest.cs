using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Enemy
{
    [SerializeField] Sprite openSprite, closedSprite;
    [SerializeField] SpriteRenderer myRender;
    
    protected override void Died(){
        base.Died();
        myRender.sprite = openSprite;
    }
}
