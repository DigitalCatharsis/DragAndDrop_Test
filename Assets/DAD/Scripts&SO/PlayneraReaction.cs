using UnityEngine;
using UnityEngine.Events;

namespace DAD
{
    public class PlayerReaction : MonoBehaviour
    {
        private GameObject _currentFace;
        private AppleReciver _appleReceiver; // Добавлена переменная для ссылки на AppleReciver

        [Header("Setup")]
        [Space(10)]
        [Header("Audio")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _appleReceivedAudioClip;
        [SerializeField] private AudioClip _allApplesReceivedAudioClip;
        [Header("VFX names")]
        [SerializeField] private string _allApplesReceivedVFX;
        [SerializeField] private string _appleReceivedVFX;
        [Header("Faces")]
        [SerializeField] private GameObject _defaultFace;
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
            PlaySound(_appleReceivedAudioClip);
            ChangeFaceTo(_onReceiveAppleFace);
        }

        private void OnAllApplesReceivedHandler()
        {
            PlaySound(_allApplesReceivedAudioClip);
            ChangeFaceTo(_onReceiveAllApplesFace);
        }

        private void PlaySound(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }

        private void ChangeFaceTo(GameObject newFace)
        {
            _currentFace.SetActive(false);
            newFace.SetActive(true);
            _currentFace = newFace;
        }
    }
}