using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private RectTransform rectT; 

    private void Awake()
    {
        rectT = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        rectT.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}
