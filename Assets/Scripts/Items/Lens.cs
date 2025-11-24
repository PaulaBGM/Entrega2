using UnityEngine;

public class Lens : MonoBehaviour
{
    [SerializeField]
    private Transform smallSheet, bigSheet;

    // Update is called once per frame
    void Update()
    {
        bigSheet.position = smallSheet.position * 2 - transform.position;
    }
}
