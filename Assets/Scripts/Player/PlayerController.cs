using System.Collections;
using Input;
using Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private ISelectable _selectableSelected;
        private bool _canSelect = true;

        // Flag para procesar el clic en Update
        private bool pendingClick = false;

        private void OnEnable()
        {
            if (InputSystemHandler.Instance != null)
            {
                InputSystemHandler.Instance.SubscribeToSelect(OnSelectInput);
                InputSystemHandler.Instance.SubscribeToInteract(OnInteract);
            }
            else
            {
                InputSystemHandler.OnInitialized += InputSystemInitialized;
            }
        }

        private void InputSystemInitialized()
        {
            InputSystemHandler.Instance.SubscribeToSelect(OnSelectInput);
            InputSystemHandler.Instance.SubscribeToInteract(OnInteract);
            InputSystemHandler.OnInitialized -= InputSystemInitialized;
        }

        // Callback del Input System – SOLO activa un flag
        private void OnSelectInput()
        {
            pendingClick = true;
        }

        // Procesamos el clic en Update (cuando la UI YA fue procesada)
        private void Update()
        {
            if (!pendingClick)
                return;

            pendingClick = false;

            HandleSelect();
        }

        private void HandleSelect()
        {
            // Ahora sí funciona IsPointerOverGameObject
            if (EventSystem.current != null)
            {
                if (EventSystem.current.IsPointerOverGameObject(0) ||
                    EventSystem.current.IsPointerOverGameObject(-1))
                {
                    return;
                }
            }

            if (!_canSelect)
                return;

            StartCoroutine(SelectionCooldown(0.5f));

            if (_selectableSelected != null)
            {
                _selectableSelected.Deselect();
                _selectableSelected = null;
                return;
            }

            Vector2 mousePos =
                Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

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

            if (_selectableSelected is MonoBehaviour mb &&
                mb.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
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

            if (InputSystemHandler.Instance != null)
            {
                InputSystemHandler.Instance.UnsubscribeToSelect(OnSelectInput);
                InputSystemHandler.Instance.UnsubscribeToInteract(OnInteract);
            }
        }
    }
}
