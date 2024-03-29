﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaObjectsHandler : MonoBehaviour
{
    public GameObject prefab;
    private ArrayList arenaObjects;

    // Arraylist of elements of type ObjectSliceCouple
    public ArrayList objectSliceCouples;

    // To know if positions have been already given
    private bool arePositionAlreadyAssigned = false;

    // Singleton of the ArenaObjectsHandler class
    public static ArenaObjectsHandler singleton = null;

    /* <summary>
     * The function is called when the component is instantiated
     * </summary>
     */
    private void Awake()
    {
        if ((singleton != null) && (singleton != this))
        {
            Destroy(this);
            return;
        }
        singleton = this;
    }


    /* <summary>
    * This function creates randomly the instruments that will be used during the game in the context mode. 
    * </summary>
    * <param name="numElems"></param> */
    public void CreateInstruments(int numElems)
    {
        this.arenaObjects = new ArrayList();
        var random = new System.Random();

        Array enumArray = Enum.GetValues(typeof(Instruments));
        ArrayList instrNames = new ArrayList(enumArray);

        //Dictionary<int, Vector3Ser> dict = ArenaObjectsRetriever.Read();

        //Instatiate the number of elements depending on the difficulty of the task.
        for (int i = 1; i <= numElems; i++)
        {
            GameObject newInstr = Instantiate(prefab);

            int index = random.Next(0, instrNames.Count);

            //Assign a name to the object in context mode -> than also an image.
            newInstr.name = instrNames[index].ToString();
            instrNames.RemoveAt(index);
            
            //Load the sprite associated with the instruments
            newInstr.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.pathToSpritesInstrumaps + newInstr.name);

            this.arenaObjects.Add(newInstr);
        }
        
        this.arenaObjects = ShuffleObjectsArraListt(this.arenaObjects);
        CombineObjectsWithSlices();
        SetArenaPositions();
        
    }

    /* <summary>
    * This function creates randomly the instruments that will be used during the game in the context mode. 
    * </summary>
    * <param name="numElems"></param> */
    public void CreateShapes(int numElems)
    {
        this.arenaObjects = new ArrayList();
        var randomShapes = new System.Random();

        Array enumArray = Enum.GetValues(typeof(GeometricalShapes));
        ArrayList shapeNames = new ArrayList(enumArray);

        //Dictionary<int, Vector3Ser> dict = ArenaObjectsRetriever.Read();

        for (int i = 1; i <= numElems; i++)
        {
            GameObject newShape = Instantiate(prefab);

            int index = randomShapes.Next(0, shapeNames.Count);

            //Assign a name to the object in context mode -> than also an image.
            newShape.name = shapeNames[index].ToString();
            shapeNames.RemoveAt(index);

            //Load the sprite associated with the instruments
            newShape.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.pathToSpritesInstrumaps_shapes + newShape.name);

            this.arenaObjects.Add(newShape);
        }

        this.arenaObjects = ShuffleObjectsArraListt(this.arenaObjects);
        CombineObjectsWithSlices();
        SetArenaPositions();
    }


    /// <summary>
    /// Set the initial position of the objects on the arena, which is the exact position that the user has to remember to win the game.
    /// </summary>
    private void CombineObjectsWithSlices()
    {
        ArrayList slices = ArenaSetup.singleton.GetSlices();

        this.objectSliceCouples = new ArrayList();

        int sliceIndex = 0;

        for (int objectIndex = 0 ; objectIndex < this.arenaObjects.Count; objectIndex++)
        {
            GameObject adjacent = ObjectInAdjacentList((GameObject)slices[sliceIndex]);

            if (adjacent != null){

                this.objectSliceCouples.Add(new ObjectSliceCouple((GameObject)slices[sliceIndex], (GameObject)this.arenaObjects[objectIndex]));
                this.objectSliceCouples.Add(new ObjectSliceCouple(adjacent, (GameObject)this.arenaObjects[objectIndex]));
                sliceIndex++;
            }
            else
            {
                this.objectSliceCouples.Add(new ObjectSliceCouple((GameObject)slices[sliceIndex], (GameObject)this.arenaObjects[objectIndex]));
            }

            sliceIndex++;
        }
    }


    /// <summary>
    /// Controls if the passed slice is in the adjacentSet.
    /// </summary>
    /// <param name="slice"></param>
    /// <returns></returns>
    public GameObject ObjectInAdjacentList(GameObject slice)
    {
        ArrayList adjacentSlices = ArenaSetup.singleton.GetDoubleSlices(); 

        //Control of the adjacent slices.
        for (int j = 0; j < adjacentSlices.Count; j++)
        {
            Slice tempAdjacent = (Slice)adjacentSlices[j];

            if (slice == tempAdjacent.slice)
            {
                return tempAdjacent.adjacentSlice;
            }
        }

        return null;
    }


    /// <summary>
    /// This method is useful to shuffle created objects to randomly assign them to precise slice.
    /// </summary>
    /// <param name="listToShuffle"></param>
    /// <returns></returns>
    private ArrayList ShuffleObjectsArraListt(ArrayList listToShuffle)
    {
        ArrayList randomList = new ArrayList();

        var rnd = new System.Random();
        int randomIndex = 0;
        while (listToShuffle.Count > 0)
        {
            randomIndex = rnd.Next(0, listToShuffle.Count); //Choose a random object in the list
            randomList.Add(listToShuffle[randomIndex]); //add it to the new, random list
            listToShuffle.RemoveAt(randomIndex); //remove to avoid duplicates
        }

        return randomList; //return the new random list
    }

    /// <summary>
    /// Shuffle drag and drop positions to not have always the same position outside the arena associated to the same slice.
    /// Moreover it takes only the positions based on the difficulty, so not all the positions.
    /// </summary>
    /// <param name="dictToShuffle"></param>
    /// <returns></returns>
    private List<Vector3Ser> ShufflePositionDictionary(Dictionary<int, Vector3Ser> dictToShuffle)
    {
        List<Vector3Ser> allValuesToShuffle = new List<Vector3Ser>(dictToShuffle.Values);
        List<Vector3Ser> valuesToShuffle = new List<Vector3Ser>();
        var rnd = new System.Random();
        int randomIndex = 0;

        for (int i = 0; i < Game1Parameters.Difficulty; i++)
        {
            valuesToShuffle.Add(allValuesToShuffle[i]);
        }

        List<Vector3Ser> randomValues = new List<Vector3Ser>();

        while (valuesToShuffle.Count > 0)
        { 
            randomIndex = rnd.Next(0, valuesToShuffle.Count); //Choose a random couple in the dict    
            randomValues.Add(valuesToShuffle[randomIndex]); //add it to the new, random dict
            valuesToShuffle.RemoveAt(randomIndex); //remove the index to avoid duplicates
        }

        return randomValues;
    }


    /// <summary>
    /// This method set the initial position of each object for each slice and it takes into account also the different cases in which slices have 
    /// to be considered together and the object is moved a bit from is natural position (just to have a better distribution of the space).
    /// </summary>
    public void SetArenaPositions()
    {        
        // If positions have been already assigned we simply recover them
        if (this.arePositionAlreadyAssigned)
        {
            foreach (ObjectSliceCouple couple in this.objectSliceCouples)
            {
                couple.arenaObject.transform.position = couple.arenaPosition;
                couple.arenaObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else
        {
            Dictionary<int, Vector3Ser> positions = ArenaObjectsRetriever.RetrievePositions(true);

            ArrayList adjacents = ArenaSetup.singleton.GetDoubleSlices();

            this.SetZeroPosition();

            int currentSliceIndex = 1;
            int adjacentSliceIndex = 0;
            int objectIndex = 0;

            for (int i = 0; i < this.objectSliceCouples.Count; i++)
            {
                if (adjacents.Count != 0)
                {
                    if (((ObjectSliceCouple)this.objectSliceCouples[i]).slice == ((Slice)adjacents[adjacentSliceIndex]).slice)
                    {
                        float x = (positions[currentSliceIndex].x + positions[currentSliceIndex + 1].x) / 3;
                        float y = (positions[currentSliceIndex].y + positions[currentSliceIndex + 1].y) / 3;
                        float z = (positions[currentSliceIndex].z + positions[currentSliceIndex + 1].z) / 3;

                        ((GameObject)this.arenaObjects[objectIndex]).transform.position = new Vector3(x, y, z);
                        ((ObjectSliceCouple)this.objectSliceCouples[i]).arenaPosition = new Vector3(x, y, z);
                        ((ObjectSliceCouple)this.objectSliceCouples[i + 1]).arenaPosition = new Vector3(x, y, z);

                        currentSliceIndex += 2;
                        adjacentSliceIndex += 2;
                        i++;
                    }
                    else
                    {
                        float x = positions[currentSliceIndex].x;
                        float y = positions[currentSliceIndex].y;
                        float z = positions[currentSliceIndex].z;

                        ((GameObject)this.arenaObjects[objectIndex]).transform.position += new Vector3(x, y, z);

                        if (Game1Parameters.Difficulty <= 4)
                        {
                            ((GameObject)this.arenaObjects[objectIndex]).transform.position /= 1.5f;
                        }

                        if (Game1Parameters.Difficulty > 4 && Game1Parameters.Difficulty <= 8 && currentSliceIndex > 4)
                        {
                            ((GameObject)this.arenaObjects[objectIndex]).transform.position /= 1.2f;
                        }

                        ((ObjectSliceCouple)this.objectSliceCouples[i]).arenaPosition = ((GameObject)this.arenaObjects[objectIndex]).transform.position;
                        currentSliceIndex++;
                    }
                }
                else
                {
                    float x = positions[currentSliceIndex].x;
                    float y = positions[currentSliceIndex].y;
                    float z = positions[currentSliceIndex].z;

                    ((GameObject)this.arenaObjects[objectIndex]).transform.position += new Vector3(x, y, z);

                    if (Game1Parameters.Difficulty <= 4)
                    {
                        ((GameObject)this.arenaObjects[objectIndex]).transform.position /= 1.5f;
                    }

                    if (Game1Parameters.Difficulty > 4 && Game1Parameters.Difficulty <= 8 && currentSliceIndex > 4)
                    {
                        ((GameObject)this.arenaObjects[objectIndex]).transform.position /= 1.2f;
                    }

                    ((ObjectSliceCouple)this.objectSliceCouples[i]).arenaPosition = ((GameObject)this.arenaObjects[objectIndex]).transform.position;
                    currentSliceIndex++;
                }

                objectIndex++;
            }
        }
    }

    /// <summary>
    /// This Method set the position from which the user has to drag objects inside the arena
    /// </summary>
    public void SetDragAndDropPositions()
    {
        this.checkAlreadyPlacedObjects();

        // This in case positions have been already assigned
        if (this.arePositionAlreadyAssigned)
        {
            foreach (ObjectSliceCouple couple in this.objectSliceCouples)
            {
                if (!couple.isPlaced)
                {
                    couple.arenaObject.transform.position = couple.dragAndDropPosition;
                    couple.arenaObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
            }
        }
        else
        {
            Dictionary<int, Vector3Ser> positions = ArenaObjectsRetriever.RetrievePositions(false);
            List<Vector3Ser> positionValues = this.ShufflePositionDictionary(positions);

            for (int i = 0; i < this.arenaObjects.Count; i++)
            {
                float x = positionValues[i].x;
                float y = positionValues[i].y;
                float z = positionValues[i].z;

                ((GameObject)this.arenaObjects[i]).transform.position = new Vector3(x, y, z);
                ((GameObject)this.arenaObjects[i]).transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

                foreach (ObjectSliceCouple couple in this.objectSliceCouples)
                {
                    if (couple.arenaObject == (GameObject)this.arenaObjects[i])
                    {
                        couple.dragAndDropPosition = ((GameObject)this.arenaObjects[i]).transform.position;
                    }
                }
            }

            this.arePositionAlreadyAssigned = true;
        }
        
    }

    /// <summary>
    /// Set the position of the objects in (0,0,0)
    /// </summary>
    private void SetZeroPosition()
    {
        for(int i = 0; i < this.arenaObjects.Count; i++)
        {
            ((GameObject)this.arenaObjects[i]).transform.position = new Vector3(0, 0, 0);
        }       
    }


    /// <summary>
    /// Check already placed objects and update the arraylist objectSliceCouples
    /// </summary>
    private void checkAlreadyPlacedObjects()
    {
        CollisionDetector script;

        foreach (ObjectSliceCouple couple in this.objectSliceCouples)
        {
            script = (CollisionDetector)couple.arenaObject.GetComponent(typeof(CollisionDetector));
            if (script.isPlaced)
            {
                couple.isPlaced = true;
            }
        }
    }

}



/// <summary>
/// Class used to wrap slice and object in a single object.
/// </summary>
class ObjectSliceCouple
{
    public GameObject slice;
    public GameObject arenaObject;
    public Vector3 arenaPosition;
    public Vector3 dragAndDropPosition;
    public bool isPlaced = false;

    public ObjectSliceCouple(GameObject slice, GameObject arenaObject)
    {
        this.slice = slice;
        this.arenaObject = arenaObject;
    }


}