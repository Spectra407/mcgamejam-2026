using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class AnimalMovement : MonoBehaviour
{
    [Header("Movement")]
    public int targetRange;
    public float baseSpeed;
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
    
    private float speed;
    private Vector2 targetPosition;
    private bool waiting;
    private int moveCount;
    private bool wobbleDirection;
    private float angleState;
    private bool hunting;
    private bool hunted;
    private GameObject hunter;
    private GameObject targetAnimal;
    
    
    void Start()
    {
        speed = baseSpeed;
        waiting = false;
        hunting = false;
        setNewTargetPosition();
    }
    
    void Update()
    {
        //HUNTING
        if (!hunting) {
            List<GameObject> animalsInRange = new  List<GameObject>();
            // Cast rays and get all objects hit
            Vector2[] directions = new Vector2[] {
                new Vector2(1, 0),  new Vector2(0, 1), new Vector2(-1, 0),  new Vector2(0, -1),
                new Vector2(1, 1), new Vector2(-1, -1), new Vector2(1, -1), new Vector2(-1, 1) };
            foreach (Vector2 dir in directions) {
                RaycastHit2D[] h = Physics2D.RaycastAll(transform.position, dir, sightRange);
                List<RaycastHit2D> hits = new List<RaycastHit2D>(h);
                // add all objects with this script (AnimalMovement) into the list (excluding itself && hunting / hunted)
                foreach (RaycastHit2D hit in hits)
                    if (hit.collider.gameObject.GetComponent<AnimalMovement>() != null && 
                        hit.collider.gameObject != gameObject && 
                        !hit.collider.gameObject.GetComponent<AnimalMovement>().getHunting() &&
                        !hit.collider.gameObject.GetComponent<AnimalMovement>().getHunted()) animalsInRange.Add(hit.collider.gameObject);
            }
            // copy all animals with lower strength into a new list
            List<GameObject> weakerAnimals = new  List<GameObject>();
            foreach (GameObject animal in animalsInRange) {
                if (gameObject.GetComponent<AnimalAI>().CurrentStrength >= animal.GetComponent<AnimalAI>().CurrentStrength)
                    weakerAnimals.Add(animal);
            }
            // randomly select one of the remaining animals IF there is a weaker one still in range
            if (animalsInRange.Count > 0) {
                targetAnimal = weakerAnimals[Random.Range(0,  weakerAnimals.Count)];
                targetAnimal.GetComponent<AnimalMovement>().setHunted(true);
                targetAnimal.GetComponent<AnimalMovement>().setHunter(this.gameObject);
                speed = Random.Range(baseSpeed - .5f, baseSpeed + .5f);
                targetAnimal.GetComponent<AnimalMovement>().setSpeed(Random.Range(baseSpeed - .5f, baseSpeed + .5f));
                hunting = true;
            }
        }
        // if the chased animal gets out of range (or killed) the animal will stop hunting and the other animal will stop being hunted
        else {
            if (Mathf.Abs(targetAnimal.transform.position.x - transform.position.x) > sightRange ||
                Mathf.Abs(targetAnimal.transform.position.y - transform.position.y) > sightRange ||
                (Mathf.Approximately(targetAnimal.transform.position.x, transform.position.x) &&
                 Mathf.Approximately(targetAnimal.transform.position.y, transform.position.y))) {
                hunting = false;
                targetAnimal.GetComponent<AnimalMovement>().setHunted(false);
                setSpeed(baseSpeed);
                targetAnimal.GetComponent<AnimalMovement>().setSpeed(targetAnimal.GetComponent<AnimalMovement>().baseSpeed);
            }
        }
        
        // MOVEMENT
        // update target position to targets new position when hunting
        if (hunting) targetPosition = targetAnimal.transform.position;
        // update target position to opposite of hunter if hunted
        
        if (hunted) {
            float newTargetX = transform.position.x - (hunter.transform.position.x - transform.position.x);
            float newTargetY = transform.position.y - (hunter.transform.position.y - transform.position.y);
            if (newTargetX < minPlayableAreaX || newTargetX > maxPlayableAreaX ||
                newTargetY < minPlayableAreaY || newTargetY > maxPlayableAreaY) {
                hunted = false;
                setSpeed(baseSpeed);
                setNewTargetPosition();
            }
            else targetPosition = new Vector2(newTargetX, newTargetY);
        }
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

    private void setNewTargetPosition() {
        // make small movements 5-7 times
        moveCount++;
        if (moveCount < Random.Range(5, 7)) {
            // set new random target position x and y
            float targetX = Random.Range(transform.position.x - targetRange, transform.position.x + targetRange);
            float targetY = Random.Range(transform.position.y - targetRange, transform.position.y + targetRange);
            // if it goes out of range, retry
            if (targetX < minPlayableAreaX || targetX > maxPlayableAreaX ||
                targetY < minPlayableAreaY || targetY > maxPlayableAreaY) setNewTargetPosition();
            // otherwise, update target position
            else targetPosition = new Vector2(targetX, targetY);
        }
        // then make a far movement
        else setNewFarTargetPosition();
    }
    
    private void setNewFarTargetPosition() {
        moveCount = 0;
        // same as before but times 3
        float targetX = Random.Range(transform.position.x - targetRange * 3, transform.position.x + targetRange * 3);
        float targetY = Random.Range(transform.position.y - targetRange * 3, transform.position.y + targetRange * 3);
        // if it goes out of range OR is too close, retry
        if (targetX < minPlayableAreaX || targetX > maxPlayableAreaX ||
            targetY < minPlayableAreaY || targetY > maxPlayableAreaY ||
            Mathf.Abs(targetX - transform.position.x) < targetRange * 2 ||
            Mathf.Abs(targetY - transform.position.y) < targetRange * 2) setNewFarTargetPosition();
        // otherwise, update target position
        else targetPosition = new Vector2(targetX, targetY);
    }

    // getters and setters
    public void setSpeed(float newSpeed) {
        speed = newSpeed;
    }
    public void setHunted(bool value) {
        hunted = value;
    }
    public bool getHunted() {
        return hunted;
    }
    public bool getHunting() {
        return hunting;
    }
    public void setHunter(GameObject hunter) {
        this.hunter = hunter;
    }
    
    public void wobble() {
        float wobbleModifier = 1;
        if (hunting || hunted) wobbleModifier = 2f;
        float angle;
        // move wobbleState between 0 and 1 (left and right)
        if (wobbleDirection) angleState += Time.deltaTime * wobbleSpeed * wobbleModifier; 
        else angleState -= Time.deltaTime * wobbleSpeed * wobbleModifier;
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