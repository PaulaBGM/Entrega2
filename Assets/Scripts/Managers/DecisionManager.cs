using Managers;
using UnityEngine;

namespace Decisions
{
    public class DecisionManager : MonoBehaviour
    {
        private bool _decisionMade;

        private void Start()
        {
            _decisionMade = false;
        }

        public void AcceptArtwork()
        {
            if (_decisionMade)
                return;

            _decisionMade = true;

            Debug.Log("DecisionManager: Accept pressed");

            GameManager.Instance.ArtworkEvaluated(true);
        }

        public void RejectArtwork()
        {
            if (_decisionMade)
                return;

            _decisionMade = true;

            Debug.Log("DecisionManager: Reject pressed");

            GameManager.Instance.ArtworkEvaluated(false);
        }

        public void ResetDecision()
        {
            _decisionMade = false;
        }
    }
}
