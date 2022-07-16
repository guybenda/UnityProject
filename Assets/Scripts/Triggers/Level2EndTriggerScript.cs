using UnityEngine;

public class Level2EndTriggerScript : MonoBehaviour
{
    private SpriteRenderer sprite;
    private static readonly Vector3 spriteOrientation = new(90f, 0, 0);

    void Start()
    {
        var alert = new GameObject("MinimapSprite");
        alert.transform.parent = gameObject.transform;
        alert.transform.localScale = Vector3.one * 60f;
        alert.layer = 11;
        sprite = alert.AddComponent<SpriteRenderer>();
        sprite.sprite = GameManagerScript.Instance.objectiveSprite;
        sprite.transform.localPosition = Vector3.up * 500f;
    }

    void Update()
    {
        sprite.transform.eulerAngles = spriteOrientation;
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 10f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerContainer"))
        {
            GameObject.FindGameObjectWithTag("PlayerContainer").GetComponent<PlayerScript>().objective = true;
            Destroy(gameObject);
        }
    }
}
