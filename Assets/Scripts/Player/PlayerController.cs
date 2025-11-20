using System.Collections;
using Input;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vector3 interactableScaleWhenSelected = new Vector3(1.5f, 1.5f, 1.5f);
        
        private ISelectable _selectableSelected;
        
        private bool _canSelect = true;
        
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
            if (!_canSelect)
                return;
            
            StartCoroutine(SelectionCooldown(0.5f));
            
            if (_selectableSelected is not null)
            {
                _selectableSelected.Deselect();
                _selectableSelected = null;
                return;
            }
            
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Collider2D col = Physics2D.OverlapPoint(mousePos);
            
            if (col != null && col.TryGetComponent(out ISelectable selectable))
            {
                _selectableSelected = selectable;
                selectable.Select();
            }
        }
        
        
        private void OnInteract()
        {
            if (_selectableSelected == null)
                return;
            
            var mb = _selectableSelected as MonoBehaviour;
            if (mb is not null && mb.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
            
            if (mb is null)
            {
                Debug.LogError(_selectableSelected + " is not a game object.");
            }
        }


        private IEnumerator SelectionCooldown(float cooldownTime)
        {
            _canSelect = false;
            yield return new WaitForSeconds(cooldownTime);
            _canSelect = true;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            
            InputSystemHandler.Instance.UnsubscribeToSelect(OnSelect);
            InputSystemHandler.Instance.UnsubscribeToInteract(OnInteract);
        }
    }
}
