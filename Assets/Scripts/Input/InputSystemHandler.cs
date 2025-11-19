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
        }

        private void OnSelectionStarted(InputAction.CallbackContext obj)
        {
            _onSelect?.Invoke();
        }

        private void OnDisable()
        {
            _playerInput.actions["Select"].started -= OnSelectionStarted;
        }

        #region Subscriptions

        public void SubscribeToSelect(Action actionToSubscribe) => _onSelect += actionToSubscribe;
        public void UnsubscribeToSelect(Action actionToUnsubscribe) => _onSelect -= actionToUnsubscribe;
    
        #endregion
    }
}
