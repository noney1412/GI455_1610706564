#region metadata
/**
GI455_Project 
by CHANON_PANPILA_1610706564
author: https://github.com/noney1412

NOTE: 
    - Unity Version (2020.2.1f1), .NET 4.6, C# 8.0
**/
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace I
{
    public class SwapImage : MonoBehaviour
    {
        public Sprite On;
        public Sprite Off;

        private Image _image;
        private Toggle _toggle;

        private void Start()
        {
            _toggle = GetComponent<Toggle>();
            _image = GetComponent<Image>();
            _image.sprite = On;
            _toggle.onValueChanged.AddListener(Swap);
        }

        public void Swap(bool state)
        {
            print(state);
            // if (state)
            // {
            //     _image.sprite = Off;
            // }
            // else
            // {
            //     _image.sprite = On;
            // }
        }
    }
}