using System.Collections;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    [Header("Movement")]
    public int targetRange;
    public float speed;
    public float activeness;
    public float maxWaitTime;
    public float sightRange;
    
    [Header("Wobble Sprite")]
    public float wobbleAmount;
    public float wobbleSpeed;
    public AnimationCurve wobbleLerp;
    
    [Header("Board Size")]
    public float minPlayableAreaX;
    public float maxPlayableAreaX;
    public float minPlayableAreaY;
    public float maxPlayableAreaY;
    
    private Vector2 targetPosition;
    private bool waiting;
    private bool wobbleDirection;
    private float angleState;
    private bool hunting;
    

    private AnimalData animalData;
    
    void Start()
    {
        animalData = GetComponent<AnimalData>();
        
        waiting = false;
        setNewTargetPosition();
    }
    
    void Update()
    {
        //HUNTING
        
        
        // MOVEMENT
        // move animal
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        
        // if target position is reached, set a new target position
        if (Mathf.Approximately(transform.position.x, targetPosition.x) &&
            Mathf.Approximately(transform.position.y, targetPosition.y) && !waiting) {
            // if random value between 0 and 1 is less than activeness, move immediately. Otherwise, wait before setting new target.
            if (Random.Range(0f, 1f) < activeness) setNewTargetPosition();
            else StartCoroutine(waitThenSetNewTargetPosition());
        }

        wobble();
    }

    IEnumerator waitThenSetNewTargetPosition() {
        waiting = true;
        yield return new WaitForSeconds(Random.Range(0f, maxWaitTime));
        waiting = false;
        setNewTargetPosition();
    }

    public void setNewTargetPosition() {
        // set new random target position x and y
        float targetX = Random.Range(transform.position.x - targetRange, transform.position.x + targetRange);
        float targetY = Random.Range(transform.position.y - targetRange, transform.position.y + targetRange);
        // if it goes out of range, retry
        if (targetX < minPlayableAreaX || targetX > maxPlayableAreaX ||
            targetY < minPlayableAreaY || targetY > maxPlayableAreaY) setNewTargetPosition();
        // otherwise, update target position
        else targetPosition = new Vector2(targetX, targetY);
    }

    public void wobble() {
        float angle;
        // move wobbleState between 0 and 1 (left and right)
        if (wobbleDirection) angleState += Time.deltaTime * wobbleSpeed; 
        else angleState -= Time.deltaTime * wobbleSpeed;
        // if reach either end of the lerp, clamp and go other direction
        if (angleState > 1f) {
            angleState = 1f; 
            wobbleDirection = false;
        }
        if (angleState < 0f) {
            angleState = 0f; 
            wobbleDirection = true;
        }
        // evaluate curve based on state and apply rotation
        angle = (wobbleLerp.Evaluate(angleState) -.5f) * wobbleAmount *2;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}