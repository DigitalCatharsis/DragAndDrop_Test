using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace DAD
{    public class SpriteColorChanger : MonoBehaviour
    {
        private Image _image;

        // animate the game object from -1 to +1 and back
        [SerializeField] private float _alphaMin = 0.30F;
        [SerializeField] private float _alphaMax = 0.9F;

        // starting value for the Lerp
        [SerializeField] private float _startTime = 0.0f;
        [SerializeField] private float _multiply = 0.1f;

        private void Start()
        {
            _image = GetComponent<Image>();
        }

        private void Update()
        {
            _image.color = new Vector4(_image.color.r, _image.color.g, _image.color.b, Mathf.Lerp(_alphaMin, _alphaMax, _startTime));

            _startTime += _multiply * Time.deltaTime;

            if (_startTime > 1.0f)
            {
                var temp = _alphaMax;
                _alphaMax = _alphaMin;
                _alphaMin = temp;
                _startTime = 0.0f;
            }
        }
    }
}