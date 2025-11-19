using Input;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private void OnEnable()
        {
            if (InputSystemHandler.Instance != null)
            {
                InputSystemHandler.Instance.SubscribeToSelect(OnSelect);
            }
            else
            {
                InputSystemHandler.OnInitialized += InputSystemInitialized;
            }
        }
    
        private void InputSystemInitialized()
        {
            InputSystemHandler.Instance.SubscribeToSelect(OnSelect);
            InputSystemHandler.OnInitialized -= InputSystemInitialized;
        }

        private void OnSelect()
        {
        }

        private void OnDisable()
        {
            InputSystemHandler.Instance.UnsubscribeToSelect(OnSelect);
        }
    }
}
