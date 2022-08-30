using UnityEngine;

[CreateAssetMenu(fileName = "FoxData", menuName = "Game Data/Fox Data")]
public class FoxData : ScriptableObject
{
    public float maxSpeed = 3;
    public float speed = 50f;
    public float swimSpeed = 25f;
    public float jumpPower = 150f;
    public bool grounded;
    public Rigidbody2D rb2d;
}
