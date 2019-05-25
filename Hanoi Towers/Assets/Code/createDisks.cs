using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createDisks : MonoBehaviour
{
    public GameObject diskPrefab;
    public int numberDisks = 5;
    public float yPos = 0.1f;
    public float height = 2f;
    public float scaling = 1.01f;
    private int index;
    static public Stack source;
    static public Stack intermediate;
    static public Stack destination;
    public int pinPosition = 12;
    // Start is called before the first frame update
    void Start()
    {
      SpawnDisks();
      StartCoroutine(init());
    }

    void SpawnDisks ()
    {
      source = new Stack();
      intermediate = new Stack();
      destination = new Stack ();

      for (index = 1; index <= numberDisks; index++)
      {
        GameObject disco = Instantiate(diskPrefab) as GameObject;
        disco.transform.position = new Vector3 (-pinPosition, yPos+(height*(index-1)) ,0);
        disco.transform.localScale += new Vector3 ((numberDisks-index)*scaling, 0, (numberDisks-index)*scaling);
        source.Push(disco);
      }

      source.Push(0);
      intermediate.Push(1);
      destination.Push(2);
      /*tower(numberDisks, source, intermediate, destination);*/
    }

    void moveDisk(Stack orig, Stack dest)
    {
         int idOrig = (int)orig.Pop();
         int idDest = (int)dest.Pop();

         int xPos = 0;
         switch (idDest)
         {
           case 0:
            xPos = -pinPosition;
            break;
           case 1:
            xPos = 0;
            break;
           case 2:
            xPos = pinPosition;
            break;
         }

         GameObject auxObject = (GameObject) orig.Pop();
         auxObject.transform.position = new Vector3 (xPos, yPos+(height*dest.Count), 0);
         dest.Push(auxObject);
         dest.Push(idDest);
         orig.Push(idOrig);


        /*public int amount = dest.Count();
        GameObject aux = dest.Pop();
        aux.transform.position = new Vector3(posX, yPos+(height*(amount)), 0); */
      /*3 pins: 0 1 2
        now that we know how many there are in each pile
        simply multiply that value by height to know how high to put a disk

      */
    }

    IEnumerator tower (int numberDisks, Stack source, Stack intermediate, Stack destination)
    {
        if (numberDisks == 1)
        {
          moveDisk (source, destination);
          yield return new WaitForSecondsRealtime(1);
        }

        else
        {
            StartCoroutine(tower (numberDisks - 1, source, destination, intermediate));
            moveDisk(source, destination);
            StartCoroutine(tower (numberDisks - 1, intermediate, source, destination));
        }

        yield return new WaitForSeconds(1);
        print ("here");
    }

    IEnumerator init ()
    {
      yield return new WaitForSeconds(2);
      StartCoroutine(tower(numberDisks, source, intermediate, destination));
    }
    // Update is called once per frame
    void Update()
    {
    }
}
