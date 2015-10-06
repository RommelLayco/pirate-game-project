using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;


/**
    This code use:
    http://gamedevelopment.tutsplus.com/tutorials/how-to-use-bsp-trees-to-generate-game-maps--gamedev-12268
    as guide.

    This class is the space partition for where to hold the rooms

**/

public class Leaf : MonoBehaviour
{

    private const int MIN_LEAF_SIZE = 6;

    public int y, x, width, height; // the position and size of this Leaf

    public Leaf leftChild; // the Leaf's left child Leaf
    public Leaf rightChild; // the Leaf's right child Leaf
    public Room room; // the room that is inside this Leaf
    public Vector3 halls; // hallways to connect this Leaf to other Leafs

    //Constructor
    public Leaf(int x, int y, int width, int height)
    {
        // initialize our leaf
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }


    public bool split()
    {
        // begin splitting the leaf into two children
        if (leftChild != null || rightChild != null)
        {
            // we're already split! Abort!
            return false;
        }


        // determine direction of split
        // if the width is >25% larger than height, we split vertically
        // if the height is >25% larger than the width, we split horizontally
        // otherwise we split randomly
        bool splitH = Random.Range(1, 101) > 50;

        if (width > height && width / height >= 1.25)
        {
            splitH = false;
        }
        else if (height > width && height / width >= 1.25)
        {
            splitH = true;
        }

        //assign height to the max value if splitH is true
        int max = (splitH ? height : width) - MIN_LEAF_SIZE;
        if (max <= MIN_LEAF_SIZE)
        {
            // the area is too small to split any more
            return false;
        }


        int split = Random.Range(MIN_LEAF_SIZE, max); // determine where we're going to split

        // create our left and right children based on the direction of the split
        if (splitH)
        {
            leftChild = new Leaf(x, y, width, split);
            rightChild = new Leaf(x, y + split, width, height - split);
        }
        else
        {
            leftChild = new Leaf(x, y, split, height);
            rightChild = new Leaf(x + split, y, width - split, height);
        }
        return true; // split successful!
    }
}
