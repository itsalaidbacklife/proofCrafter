using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shading : MonoBehaviour
{
    private int width;
    private int height;

    private Color[][] colors;

    public Color fillColor = new Color(0.8f,0.1f,0.1f,1.0f);
    public Color emptyColor = Color.white;

    private int size;

    public Color borderColor = Color.black;

    public Vector2[] regions; //0-6

    public Problem[] problems;

    public Text problemText;

    public int problemNumber = 0;

    private bool interractable = true;

    public Shading nextShape;

    public string nextScene = "";

    public bool firstShape;

    private bool active = false;

    private bool loaded = false;

    // Start is called before the first frame update
    void Start()
    {
        if (firstShape) {
            LoadProblem();
        } else {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    private void Fillup(int x, int y, Color ogColor, Color newColor, int depth) {
        if (depth > 40) return;
        if (x >= width || x <= 0 || y >= height || y <= 0) return;
        for (; y < height; y++) {
            if (colors[y][x].Equals(ogColor) ) {
                colors[y][x] = newColor;
                Fillright(x+1,y,ogColor,newColor, depth + 1);
                Fillleft(x-1,y,ogColor,newColor, depth + 1);
            } else if(colors[y][x].Equals(borderColor)) {
                return;
            }
        }
    }

    private void Filldown(int x, int y, Color ogColor, Color newColor, int depth) {
        if (depth > 40) return;
        if (x >= width || x <= 0 || y >= height || y <= 0) return;
        for (; y > 0; y--) {
            if (colors[y][x].Equals(ogColor) ) {
                colors[y][x] = newColor;
                Fillright(x+1,y,ogColor,newColor, depth + 1);
                Fillleft(x-1,y,ogColor,newColor, depth + 1);
            } else if(colors[y][x].Equals(borderColor)) {
                return;
            }
        }
    }

    private void Fillleft(int x, int y, Color ogColor, Color newColor, int depth) {
        if (depth > 40) return;
        if (x >= width || x <= 0 || y >= height || y <= 0) return;
        for (; x > 0; x--) {
            if (colors[y][x].Equals(ogColor) ) {
                colors[y][x] = newColor;
                Fillup(x,y+1,ogColor,newColor, depth + 1);
                Filldown(x,y-1,ogColor,newColor, depth + 1);
            } else if(colors[y][x].Equals(borderColor)) {
                return;
            }
        }
    }

    private void Fillright(int x, int y, Color ogColor, Color newColor, int depth) {
        if (depth > 40) return;
        if (x >= width || x <= 0 || y >= height || y <= 0) return;
        for (; x < width; x++) {
            if (colors[y][x].Equals(ogColor) ) {
                colors[y][x] = newColor;
                Fillup(x,y+1,ogColor,newColor, depth + 1);
                Filldown(x,y-1,ogColor,newColor, depth + 1);
            } else if(colors[y][x].Equals(borderColor)) {
                return;
            }
        }
    }

    private void TryFill(int x, int y, Color ogColor, Color newColor, Queue<Vector2> Q) {
        if (x >= width || x <= 0 || y >= height || y <= 0) return;
        if (ogColor.Equals(borderColor)) return;
        if (!colors[y][x].Equals(ogColor)) return;
        if (colors[y][x].a < 0.5f) return;
        colors[y][x] = newColor;
        Q.Enqueue(new Vector2(x,y));
    }

    private void Fill(int x, int y, Color ogColor, Color newColor) {
        if (x >= width || x <= 0 || y >= height || y <= 0) return;
        if (ogColor.Equals(borderColor)) return;
        if (colors[y][x].a < 0.5f) return;
        /*Fillup(x,y+1,ogColor,newColor, 0);
        Filldown(x,y-1,ogColor,newColor, 0);

        Fillleft(x-1,y,ogColor,newColor, 0);
        Fillright(x,y,ogColor,newColor, 0);*/
        colors[y][x] = newColor;
        Queue<Vector2> Q = new Queue<Vector2>();
        Q.Enqueue(new Vector2(x,y));
        while(Q.Count > 0) {
            Vector2 pt = Q.Dequeue();
            TryFill((int)pt.x+1,(int)pt.y,ogColor,newColor,Q);
            TryFill((int)pt.x-1,(int)pt.y,ogColor,newColor,Q);
            TryFill((int)pt.x,(int)pt.y+1,ogColor,newColor,Q);
            TryFill((int)pt.x,(int)pt.y-1,ogColor,newColor,Q);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active && Input.GetMouseButtonDown (0) && interractable) {
            print("Mouse down");
            // Get Mouse position - convert to world position
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    
            screenPos = new Vector2(screenPos.x, screenPos.y);
            print("posx: " + screenPos.x + ", posy: " + screenPos.y);

            // Check if we clicked on our object
            RaycastHit2D[] ray = Physics2D.RaycastAll(screenPos, Vector2.zero, 0.01f);
            print(ray.Length);
            for (int i = 0; i < ray.Length; i++)
            {
                print(ray[i]);
                // You will want to tag the image you want to lookup
                if (ray[i].collider.tag == "shading" && ray[i].collider.gameObject == gameObject)
                { 
                    print("Collided");
                    // Set click position to the gameobject area
                    screenPos -= ray[i].collider.gameObject.transform.position;

                    print("Screenx: " + screenPos.x + ", Screeny: " + screenPos.y);
                    int x = (int)(screenPos.x * size / transform.localScale.x);
                    int y = (int)(screenPos.y * size / transform.localScale.y);

                    print("Screenx: " + x + ", Screeny: " + y);
                    print("width: " + width + ", height: " + height);
                    // Get color data
                    if (x > 0 && x < width && y > 0 && y < height)
                    {
                        Color startColor = colors[y][x];
                        if (startColor.Equals(emptyColor)) {
                            Fill(x,y,startColor,fillColor);
                        } else if (startColor.Equals(fillColor)) {
                            Fill(x,y,startColor,emptyColor);
                        }

                    print("Texture filled...");
                        Texture2D texture = new Texture2D(size, size);
                        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, size, size), Vector2.zero, size);
                        GetComponent<SpriteRenderer>().sprite = sprite;

                        for (y = 0; y < height; y++)
                        {
                            for (x = 0; x < width; x++) //Goes through each pixel
                            {
                                texture.SetPixel(x, y, colors[y][x]);
                            }
                        }
                        texture.Apply();
                    }                   
                    break;
                }
            }
        }
    }

    public void LoadProblem() {
        if (loaded) {
            problemNumber = 0;
            ResetProblem();
            active = true;
            gameObject.GetComponent<Renderer>().enabled = true;
            return;
        }
        Sprite oldSprite = GetComponent<SpriteRenderer>().sprite;
        width = oldSprite.texture.width;
        height = oldSprite.texture.height;
        size = Mathf.Max(width, height);
        colors = new Color[height][];

        for (int y = 0; y < height; y++)
        {
            colors[y] = new Color[width];
            for (int x = 0; x < width; x++) //Goes through each pixel
            {
                colors[y][x] = oldSprite.texture.GetPixel(x, y);
            }
        }

        Texture2D texture = new Texture2D(size, size);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, size, size), Vector2.zero, size);
        GetComponent<SpriteRenderer>().sprite = sprite;
     
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++) //Goes through each pixel
            {
                Color pixelColour = colors[y][x];
                texture.SetPixel(x, y, pixelColour);
            }
        }
        texture.Apply();
        problemNumber = 0;
        if (problemText != null)
            problemText.text = problems[problemNumber].description;

        loaded = true;

        active = true;
        gameObject.GetComponent<Renderer>().enabled = true;
    }

    public bool AnswerCorrect() {
        int count = 0;
        foreach (Vector2 pt in regions) {
            if (colors[(int)pt.y][(int)pt.x].Equals(fillColor)) {
                count++;
            }
        }
        print("Count " + count);
        if (count != problems[problemNumber].expectedSolution.Length) {
            return false;
        }

        foreach (int expected in problems[problemNumber].expectedSolution) {
            print("Checking " + expected);
            Vector2 pt = regions[expected];
            if (!colors[(int)pt.y][(int)pt.x].Equals(fillColor)) {
                return false;
            }
        }
        return true;
    }

    public void NextProblem() {
        problemNumber = problemNumber + 1;
        if (problemNumber >= problems.Length) {
            active = false;
            gameObject.GetComponent<Renderer>().enabled = false;
            if (nextShape != null) {
                nextShape.LoadProblem();
            } else if (nextScene != "") {
                SceneManager.LoadScene(nextScene);
            }
        } else {
            ResetProblem();
        }
    }

    public void ResetProblem() {
        problemText.text = problems[problemNumber].description;
        foreach (Vector2 pt in regions) {
            Fill((int)pt.x, (int)pt.y, fillColor, emptyColor);
        }

        Texture2D texture = new Texture2D(size, size);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, size, size), Vector2.zero, size);
        GetComponent<SpriteRenderer>().sprite = sprite;
        int x,y;
        for (y = 0; y < height; y++)
        {
            for (x = 0; x < width; x++) //Goes through each pixel
            {
                texture.SetPixel(x, y, colors[y][x]);
            }
        }
        texture.Apply();
        interractable = true;
    }

    public void OnVerify() {
        if (!interractable || !active) return;
        interractable = false;
        if (AnswerCorrect()) {
            problemText.text = "Correct!";
            Invoke("NextProblem", 3);
        } else {
            problemText.text = "Incorrect, try again";
            Invoke("ResetProblem", 3);
        }
    }

    [System.Serializable]
    public class Problem {
        [SerializeField]
        public int[] expectedSolution;

        [SerializeField]
        public string description;
    }
}
