using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

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
    public float timer = 0f;
    public bool notRan = true;
    public Camera cameraa ;
    // Start is called before the first frame update
    void Start()
    {
      SpawnDisks();
      RunTowerTask(numberDisks, source, intermediate, destination);
      /*StartCoroutine(init());*/
    }

    async void SpawnDisks ()
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

      await Task.Yield();
      await Task.Delay(1000);
      /*tower(numberDisks, source, intermediate, destination);*/
    }

    async Task moveDisk(Stack orig, Stack dest)
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

         await Task.Yield();
         await Task.Delay(1000);
         return;
    }



    async Task tower (int numberDisks, Stack source, Stack intermediate, Stack destination)
    {
        if (numberDisks == 1)
        {
          await moveDisk (source, destination);
          return;
        }

        else
        {
            await tower (numberDisks - 1, source, destination, intermediate);
            await moveDisk(source, destination);
            await tower (numberDisks - 1, intermediate, source, destination);
        }

        return;
    }

    // Update is called once per frame
    void Update()
    {


    }


    async void RunTowerTask(int numberDisks, Stack source, Stack intermediate, Stack destination)
    {
      try
      {
          await tower(numberDisks, source, intermediate, destination);
      }
      catch(Exception e)
      {
          Debug.LogException(e);
      }
    }

}
