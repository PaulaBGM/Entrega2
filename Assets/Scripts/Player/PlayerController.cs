using System;
using Input;
using Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private GameObject _interactableSelected;
        
        private void OnEnable()
        {
            if (InputSystemHandler.Instance != null)
            {
                InputSystemHandler.Instance.SubscribeToSelect(OnSelect);
                InputSystemHandler.Instance.SubscribeToInteract(OnInteract);
            }
            else
            {
                InputSystemHandler.OnInitialized += InputSystemInitialized;
            }
        }
    
        private void InputSystemInitialized()
        {
            InputSystemHandler.Instance.SubscribeToSelect(OnSelect);
            InputSystemHandler.Instance.SubscribeToInteract(OnInteract);
            InputSystemHandler.OnInitialized -= InputSystemInitialized;
        }

        private void OnSelect()
        {
            if (_interactableSelected is not null)
            {
                _interactableSelected = null;
                return;
            }
            
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Collider2D col = Physics2D.OverlapPoint(mousePos);
            
            if (col != null && col.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                _interactableSelected = col.gameObject;
                interactable.Select();
            }
        }
        
        
        private void OnInteract()
        {
            if (_interactableSelected is null)
                return;

            if (_interactableSelected.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }

        private void Update()
        {
            if (_interactableSelected is null)
                return;

            MoveInteractableSmooth();
        }
        
        private void MoveInteractableSmooth()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            
            float k = 10f;
            float t = 1 - Mathf.Exp(-k * Time.deltaTime);
            
            _interactableSelected.transform.position =
                Vector2.Lerp(_interactableSelected.transform.position, mousePos, t);
        }

        private void OnDisable()
        {
            InputSystemHandler.Instance.UnsubscribeToSelect(OnSelect);
            InputSystemHandler.Instance.UnsubscribeToInteract(OnInteract);
        }
    }
}
