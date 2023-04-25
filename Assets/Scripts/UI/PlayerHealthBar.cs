using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private void Start()
    {
        RefManager.Instance.Player.AttachHealthBar(GetComponent<Slider>());
    }
}
