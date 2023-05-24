using System.Collections;
using UnityEngine;

public class BoomPoint : MonoBehaviour
{
    [SerializeField] private float effectDuration = 0.1f;
    [SerializeField] private float pointForce = 500f;
    [SerializeField] private float colliderRadius = 5f;

    private Collider2D createdCollider;

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(ExplodeOtherObjects());
        }
    }


    private IEnumerator ExplodeOtherObjects()
    {
        CreateEffector();

        MoveTrigger();

        yield return new WaitForSeconds(effectDuration);

        Destroy(createdCollider.gameObject);
        Destroy(gameObject);
    }

    private void CreateEffector()
    {
        GameObject effectorObject = new GameObject("Effector");
        effectorObject.transform.position = transform.position;
        effectorObject.transform.parent = transform;

        createdCollider = effectorObject.AddComponent<CircleCollider2D>();
        CircleCollider2D circleCollider = (CircleCollider2D)createdCollider;
        circleCollider.isTrigger = true;
        circleCollider.usedByEffector = true;
        circleCollider.radius = colliderRadius;

        PointEffector2D effector = effectorObject.AddComponent<PointEffector2D>();
        effector.forceMagnitude = pointForce;
        effector.forceVariation = 0f;
        effector.distanceScale = 1f;
        effector.enabled = true;
        effector.forceSource = EffectorSelection2D.Rigidbody;
    }

    private void MoveTrigger()
    {
        Vector2 displacement = new(0f, 0.1f);
        createdCollider.offset += displacement;
    }
}