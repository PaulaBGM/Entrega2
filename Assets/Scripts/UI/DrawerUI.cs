using System;
using Items;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class DrawerUI : MonoBehaviour
    {
        private Button _drawerButton;
        private ItemBase _itemBase;

        [SerializeField] private GameObject collectableGameObject;

        private void Awake()
        {
            _drawerButton = GetComponent<Button>();

            _drawerButton.onClick.AddListener(OnDrawerButtonClicked);
            
            if (collectableGameObject is null || !collectableGameObject.TryGetComponent(out _itemBase))
            {
                throw new NullReferenceException(
                    "Collectable GameObject is not assigned or does not have ICollectable component.");
            }
        }

        private void OnEnable()
        {
            _itemBase.OnCollect += HandleOnCollect;
            _itemBase.OnUncollect += HandleOnUncollect;
        }
        
        private void Start()
        {
            _drawerButton.interactable = false;
        }
        
        private void HandleOnCollect()
        {
            DelayAction.Execute(() => _drawerButton.interactable = true, 0.5f);
        }
        
        private void HandleOnUncollect()
        {
            DelayAction.Execute(() => _drawerButton.interactable = false, 0.5f);
        }
        
        private void OnDrawerButtonClicked()
        {
            _drawerButton.interactable = false;
            _itemBase.Uncollect();
            collectableGameObject.transform.position = transform.position;
        }
        
        private void OnDisable()
        {
            _itemBase.OnCollect -= HandleOnCollect;
            _itemBase.OnUncollect -= HandleOnUncollect;
        }
    }
}