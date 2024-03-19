using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;


    private int emptyLocation;
    private int size;

    private List<Transform> pieces;
    private bool shuffling = false;


    // Start is called before the first frame update
    void Start()
    {
        pieces = new List<Transform>();
        size = 4;
        CreateGamePieces(0.01f);
        StartCoroutine(DelayShuffle(2.0f));
    }

    // Update is called once per frame
    void Update()
    {
        //using a raycast to detect if a piece of the puzzle is clicked
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                for (int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i] == hit.transform)
                    {
                        //check the direction to see if the tile can move.
                        //break out after a successful move
                        if (MoveIfValid(i, -size, size)) { break; }
                        if (MoveIfValid(i, +size, size)) { break; }
                        if (MoveIfValid(i, -1, 0)) { break; }
                        if (MoveIfValid(i, +1, size - 1)) { break; }
                    }
                }
            }
        }

        // win condition
        if (!shuffling && CheckCompletion())
        {
            shuffling = true;

           //load UI
        }
    }

    private bool MoveIfValid (int i, int offset, int colCheck)
    {
        if(((i % size) != colCheck) && ((i + offset) == emptyLocation))
        {
            //swap tiles in game
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            //swap their tranforms
            (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));
            //update empty location
            emptyLocation = i;
            return true;
        }
        return false;
    }
    //create game puzzle size
    private void CreateGamePieces(float gapThickness)
    {
        float width = 1 / (float)size;
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);

                // placing the pieces in the game board
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width, +1 - (2 * width * row) - width, 0);
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";

                //creating an empty space to move tiles
                if ((row == size - 1) && (col == size - 1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    //Maping the picture into bits
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];
                    // making A UV coordination order: (0, 0) (0, 1) (1, 0) (1, 1)
                    uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                    uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                    uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));

                    mesh.uv = uv;
                }
            }
        }
    }
    //display puzzle before shuffling
    private IEnumerator DelayShuffle(float duration)
    {
        yield return new WaitForSeconds(duration);
        Shuffle();
        shuffling = false;
    }

    // shuffle tiles positions at start of game
    private void Shuffle()
    {
        int count = 0;
        int last = 0;
        while (count < (size * size * size))
        {
            //Pick random location
            int rnd = Random.Range(0, size * size);
            if (rnd == last) { continue; }
            last = emptyLocation;
            // look for valid moves
            if (MoveIfValid(rnd, -size, size))
            {
                count++;
            }
            else if (MoveIfValid(rnd, +size, size))
            {
                count++;
            }
            else if (MoveIfValid(rnd, -1, 0))
            {
                count++;
            }
            else if (MoveIfValid(rnd, +1, size - 1))
            {
                count++;
            }
        }
    }

    //check if all tiles are placed in corrrect positions

    private bool CheckCompletion()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
            {
                return false;
            }

            //automatically complete level
            
        }
        return true;
    }
}