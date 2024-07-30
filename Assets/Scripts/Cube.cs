using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cube : MonoBehaviour
{
    [SerializeField] public int _count = 2;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI countText;
    public bool canDragg = true;
    public bool isGround = false;
    private bool cubeActive = false;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2d;
    private void Awake()
    {
        countText = GetComponentInChildren<TextMeshProUGUI>();    
        _spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _rigidbody2d.isKinematic = true;
        ChangeColor(_count);
        ChangeTextCount();
    }
    private void OnMouseDown()
    {
        cubeActive = true;
    }
    private void OnMouseUp()
    {
        if (canDragg)
        {
            canDragg = false;
            _rigidbody2d.isKinematic = false;
            StartCoroutine(WaitForFalling());
        }
    }
    
    private void OnMouseDrag()
    {
        if (canDragg)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _rigidbody2d.position = mousePosition;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isGround)
        {
            if (collision.gameObject.tag == "Finish")
            {
                isGround = true;
            }
        }
        
        if (collision.gameObject.tag == "Cube")
        {
            if(collision.gameObject.GetComponent<Cube>().cubeActive && canDragg)
            {
                Destroy(gameObject);
            }
            else if (collision.gameObject.GetComponent<Cube>().isGround && isGround)
            {
                if (collision.gameObject.GetComponent<Cube>()._count == _count)
                {
                    _count *= 2;
                    gameManager.listCube.Remove(collision.gameObject.GetComponent<Cube>());
                    gameManager.score += _count;
                    Destroy(collision.gameObject);
                    ChangeTextCount();
                    ChangeColor(_count);
                }
            }
            else if(collision.gameObject.GetComponent<Cube>().isGround && !isGround)
            {
                if (collision.gameObject.GetComponent<Cube>()._count == _count)
                {

                    isGround = true;
                    _count *= 2;
                    gameManager.listCube.Remove(collision.gameObject.GetComponent<Cube>());
                    gameManager.score += _count;
                    Destroy(collision.gameObject);
                    ChangeTextCount();
                    ChangeColor(_count);
                }
                else
                {
                    isGround = true;
                }
            }
        }        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            gameManager.listCube.Remove(gameObject.GetComponent<Cube>());
            Destroy(gameObject);
            gameManager.SpawnNewCube();
        }
        if (collision.gameObject.tag == "Area" && canDragg)
        {
            gameManager.listCube.Remove(gameObject.GetComponent<Cube>());
            Destroy(gameObject);
            gameManager.SpawnNewCube();
        }
        if (collision.gameObject.tag == "Area" && isGround)
        {
            gameManager.ClickRestart();
        }
    }
    
    public void ChangeColor(int count)
    {
        _spriteRenderer.material.color = gameManager.SetColorForCube(count);
    }
    private void ChangeTextCount()
    {
        countText.text = _count.ToString();
    }
    IEnumerator WaitForFalling()
    {
        yield return new WaitForSeconds(2);   
        gameManager.SpawnNewCube();
    }
}
