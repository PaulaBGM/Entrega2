using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class SmoothTransform : MonoBehaviour
{
    [SerializeField] private float _moveConstant = 10f;
    [SerializeField] private float _scaleConstant = 10f;

    public void MoveSmooth()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
        float t = 1 - Mathf.Exp(-_moveConstant * Time.deltaTime);
            
        var objectivePosition = new Vector3(mousePos.x, mousePos.y, -2f);
            
        gameObject.transform.position =
            Vector3.Lerp(gameObject.transform.position, objectivePosition, t);
    }
    
    public void ScaleSmooth(Vector3 objectiveScale)
    {
        StartCoroutine(ScaleCoroutine(objectiveScale));
    }

    private IEnumerator ScaleCoroutine(Vector3 objectiveScale)
    {
        while ((gameObject.transform.localScale - objectiveScale).sqrMagnitude > 0.0001f)
        {
            float t = 1 - Mathf.Exp(-_scaleConstant * Time.deltaTime);
                
            gameObject.transform.localScale = Vector3.Lerp(
                gameObject.transform.localScale,
                objectiveScale,
                t);
            yield return null;   
        }
    }
}
