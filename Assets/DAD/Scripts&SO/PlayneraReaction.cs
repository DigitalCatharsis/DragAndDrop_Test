using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DAD
{
    public class PlayerReaction : MonoBehaviour
    {
        private GameObject _currentFace;
        private AppleReciver _appleReceiver;
        private bool _hasFinished = false;

        [Header("Setup")]
        [Space(10)]
        [Header("Audio")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _appleReceivedAudioClip;
        [SerializeField] private AudioClip _allApplesReceivedAudioClip;
        //[Header("VFX names")]
        //[SerializeField] private string _allApplesReceivedVFX;
        //[SerializeField] private string _appleReceivedVFX;
        [Header("Faces")]
        [SerializeField] private GameObject _defaultFace;
        [SerializeField] private GameObject _defaultFace2;
        [SerializeField] private GameObject _onReceiveAppleFace;
        [SerializeField] private GameObject _onReceiveAllApplesFace;



        void OnEnable()
        {
            // Инициализация переменной appleReceiver
            _appleReceiver = FindObjectOfType<AppleReciver>();
            _appleReceiver.OnAppleDetected += OnAppleReceivedHandler;
            _appleReceiver.OnAllApplesPlaced += OnAllApplesReceivedHandler;
        }

        void OnDisable()
        {
            _appleReceiver.OnAppleDetected -= OnAppleReceivedHandler;
            _appleReceiver.OnAllApplesPlaced -= OnAllApplesReceivedHandler;
        }

        private void Awake()
        {
            _currentFace = _defaultFace;
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnAppleReceivedHandler()
        {
            StopAllCoroutines();
            StartCoroutine(Reaction(_appleReceivedAudioClip, _onReceiveAppleFace));
        }

        private void OnAllApplesReceivedHandler()
        {
            if (!_hasFinished)
            {
                _hasFinished = true;
                StopAllCoroutines();
                StartCoroutine(Reaction(_allApplesReceivedAudioClip, _onReceiveAllApplesFace));
            }
        }

        private IEnumerator Reaction(AudioClip clip, GameObject newFace)
        {
            ChangeFaceTo(newFace);
            _audioSource.PlayOneShot(clip, _audioSource.volume);
            if (_hasFinished != true)
            {
                yield return StartCoroutine(MadShake(0.1f, 0.1f, 10));
            }
            else
            {
                yield return StartCoroutine(MadShake(0.8f, 0.1f, 10));
            }
            yield return new WaitForSeconds(1.5f);
            ChangeFaceTo(_defaultFace2);
        }

        private void ChangeFaceTo(GameObject newFace)
        {
            _currentFace.SetActive(false);
            newFace.SetActive(true);
            _currentFace = newFace;
        }

        private IEnumerator MadShake(float multiply, float amplitude = 0.2f, float duration = 10)
        {
            int counter = 0;
            var savedPosition = transform.position;
            while (counter <= duration)
            {
                transform.position += new Vector3(Random.Range(0, multiply), Random.Range(0, multiply), transform.position.z);
                yield return new WaitForSeconds(amplitude);
                transform.position = savedPosition;
                counter++;
            }
        }
    }
}