using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSortingOrderEntity : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100) + (int)transform.position.z;
    }
}
