using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputSystemHandler : MonoBehaviour
    {
        public static InputSystemHandler Instance { get; private set; }
    
        public static event Action OnInitialized;
    
        private PlayerInput _playerInput;

        private Action _onSelect;
        private Action _onInteract;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
        
            Instance = this;
            _playerInput = GetComponent<PlayerInput>();
            OnInitialized?.Invoke();
        }

        private void OnEnable()
        {
            _playerInput.actions["Select"].started += OnSelectionStarted;
            _playerInput.actions["Interact"].started += OnInteractStarted;
        }

        private void OnInteractStarted(InputAction.CallbackContext obj)
        {
            _onInteract.Invoke();
        }

        private void OnSelectionStarted(InputAction.CallbackContext obj)
        {
            _onSelect?.Invoke();
        }

        private void OnDisable()
        {
            _playerInput.actions["Select"].started -= OnSelectionStarted;
            _playerInput.actions["Interact"].started -= OnInteractStarted;
        }

        #region Subscriptions

        public void SubscribeToSelect(Action actionToSubscribe) => _onSelect += actionToSubscribe;
        public void UnsubscribeToSelect(Action actionToUnsubscribe) => _onSelect -= actionToUnsubscribe;
        
        public void SubscribeToInteract(Action actionToSubscribe) => _onInteract += actionToSubscribe;
        public void UnsubscribeToInteract(Action actionToUnsubscribe) => _onInteract -= actionToUnsubscribe;
    
        #endregion
    }
}
