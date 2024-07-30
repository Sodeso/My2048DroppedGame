using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Cube firstCube;
    [SerializeField] int maxInt = 2; 
    [SerializeField] Color color2;
    [SerializeField] Color color4;
    [SerializeField] Color color8;
    [SerializeField] Color color16;
    [SerializeField] Color color32;
    [SerializeField] Color color64;
    [SerializeField] Color color128;
    [SerializeField] Color color256;
    [SerializeField] Color color512;
    [SerializeField] Color color1024;
    [SerializeField] Color color2048;
    
    [Space]

    [SerializeField] Color darkBackground;
    [SerializeField] Color lightBackground;
    public List<Cube> listCube;
    TextMeshProUGUI scoreText;
    public int score = 0;
    void Start()
    {
        scoreText = GameObject.Find("TextScore").gameObject.GetComponent<TextMeshProUGUI>();
        SpawnNewCube();
    }

    void Update()
    {
        scoreText.text = score.ToString();
        foreach (Cube cube in listCube)
        {
            if (cube._count > maxInt)
            {
                maxInt = cube._count;
            }
            if(maxInt == 2048)
            {
                Debug.Log("You win!");
            }
        }
    }
    public void SpawnNewCube()
    {
        do
        {
            firstCube._count = Random.Range(2, maxInt);
        } 
        while (SetColorForCube(firstCube._count) == Color.black);
        Cube spawnedCube = Instantiate(firstCube, firstCube.transform.parent);
        spawnedCube.gameObject.SetActive(true);
        listCube.Add(spawnedCube);
    }
    public Color SetColorForCube(int count)
    {
        switch (count)
        {
            case 2:
                return color2;
            case 4:
                return color4;
            case 8:
                return color8;
            case 16:
                return color16;
            case 32:
                return color32;
            case 64:
                return color64;
            case 128:
                return color128;
            case 256:
                return color256;
            case 512:
                return color512;
            case 1024:
                return color1024;
            case 2048:
                return color2048;
            default:
                return Color.black;
        }
    }
    public void ClickRestart()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void ClickChangeBackgroundColor()
    {
        if(Camera.main.backgroundColor == darkBackground)
            Camera.main.backgroundColor = lightBackground;
        else
            Camera.main.backgroundColor = darkBackground;
    }
}
