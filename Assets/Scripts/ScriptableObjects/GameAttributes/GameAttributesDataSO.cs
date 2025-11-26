using UnityEngine;

namespace ScriptableObjects.GameAttributes
{
    [CreateAssetMenu(fileName = "GameAttributesDataSO", menuName = "TheExpertsEye/GameAttributesDataSO")]
    public class GameAttributesDataSo : ScriptableObject
    {
        [field:SerializeField] public int Reputation { get; private set; }
        [field:SerializeField] public int Funds {get; private set; }
        [field:SerializeField] public int Ethic {get; private set; }
    }
}
