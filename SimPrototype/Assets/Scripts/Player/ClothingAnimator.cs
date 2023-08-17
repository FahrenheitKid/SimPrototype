using System.Collections.Generic;
using System.Linq;
using SimPrototype;
using UnityEngine;
using UnityEngine.U2D;

public class ClothingAnimator : MonoBehaviour
{
    [field: SerializeField] public Clothing ClothingPiece { get; private set; }
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private SpriteAtlas _spriteAtlas;

    public bool IsHair => ClothingPiece?.ClothingType == Enums.ClothingType.Hair;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _spriteAtlas = GameDatabase.Instance.ItemsDatabase.CharacterSpriteAtlas;
    }

    public void Setup(Clothing clothingPiece)
    {
        ClothingPiece = clothingPiece;
        _spriteAtlas = _spriteAtlas == null ? GameDatabase.Instance.ItemsDatabase.CharacterSpriteAtlas : _spriteAtlas;
        _renderer = _renderer == null ? GetComponent<SpriteRenderer>() : _renderer;
        Init();
    }

    void Init()
    {
        _renderer.sprite = ClothingPiece == null ? null : _spriteAtlas.GetSprite(ClothingPiece.FirstFrameSpriteName);
    }

    void LateUpdate()
    {
        
    }

    public void WearClothing(Clothing clothing)
    {
        
    }

    public void UpdateSprite(string parentSpriteName)
    {
        if(ClothingPiece == null || string.IsNullOrEmpty(parentSpriteName)) return;
        //we get the sprite index from parent. The base sprite is already "normalized" since the spritesheet is correct
        int currentParentNormalizedIndex = UtilityTools.UtilityTools.GetLastNumberFromString(parentSpriteName);
        int startingSpriteIndex = ClothingPiece.GetStartingSpriteSheetIndex();
        
        //here we need to convert the normalized index to the rest of the spritesheets that are not layout properly
        // hats I picked are okay so dont need any calculations
        int currentRawIndex = ClothingPiece.ClothingType != Enums.ClothingType.Hat ? GetIndexFromNormalizedIndex(startingSpriteIndex,currentParentNormalizedIndex,IsHair) : currentParentNormalizedIndex; 

        
        //the new sprite name will be the name of the clothing sprite with the index in sync with the parent animation walk
        string newSpriteName = ClothingPiece.GetFirstSpriteName(true);
        newSpriteName = newSpriteName + currentRawIndex;
        var newSprite = _spriteAtlas.GetSprite(newSpriteName);
        
        if (newSprite)
        {
            Sprite oldSprite = _renderer.sprite;
            Destroy(oldSprite);
            _renderer.sprite = newSprite;
        }

    }

    #region MightNotBeNeeded
    public static int GetNextSpriteIndex(int startingIndex, int currentIndex, bool isHair)
    {
        int jumpValue = isHair ? Clothing.AnimationSpriteSheetJumpHair : Clothing.AnimationSpriteSheetJump;
        
        if (Mathf.Abs((currentIndex + 1) - startingIndex) < Clothing.AnimationFramesConnectedSize)
        {
            return currentIndex+1;
        }
        else
        {
            return GetNextRowStartingIndex(startingIndex, currentIndex, isHair);
        }
    }

    public static int GetLastSpriteIndex(int startingIndex, bool isHair)
    {

        int jumpValue = isHair ? Clothing.AnimationSpriteSheetJumpHair : Clothing.AnimationSpriteSheetJump;
        // 8 connected then a 105 frame jump
        // four rows  in the sheet so we do this 3 times to reach the last index
        // this values are in the const variables if we need to change them someday
        return (startingIndex + Clothing.AnimationFramesConnectedSize + jumpValue) * Clothing.AnimationSpriteSheetTotalRowJumps; 
        
    }

    public static int GetNextRowStartingIndex(int startingIndex, int currentIndex, bool isHair)
    {
        int jumpValue = isHair ? Clothing.AnimationSpriteSheetJumpHair : Clothing.AnimationSpriteSheetJump;

        List<int> rowStartingIndexes = new List<int>(); // we exclude the first row;
        for (int i = 0; i < Clothing.AnimationSpriteSheetTotalRowJumps; i++)
        {
            rowStartingIndexes.Add(startingIndex + Clothing.AnimationFramesConnectedSize + (jumpValue *(i + 1)) );
        }

        for (int i = 0; i < Clothing.AnimationSpriteSheetTotalRowJumps; i++)
        {
            if (currentIndex >= rowStartingIndexes.ElementAt(i) &&
                (currentIndex < rowStartingIndexes.ElementAt(i) + Clothing.AnimationFramesConnectedSize))
            {
                //we are in this current row, so the next should be i +1
                int iteratorValue = i + 1 < rowStartingIndexes.Count ? i +1 : 0;
                return iteratorValue;
            }
        }
        
        // if not any of the other rows, then we should "wrap around" and restart
        return startingIndex;
    }
    
    public static int GetCurrentRowStartingIndex(int startingIndex, int currentIndex, bool isHair)
    {
        int jumpValue = isHair ? Clothing.AnimationSpriteSheetJumpHair : Clothing.AnimationSpriteSheetJump;

        List<int> rowStartingIndexes = new List<int>(); // we exclude the first row;
        for (int i = 0; i < Clothing.AnimationSpriteSheetTotalRowJumps; i++)
        {
            rowStartingIndexes.Add(startingIndex + Clothing.AnimationFramesConnectedSize + (jumpValue *(i + 1)) );
        }

        for (int i = 0; i < Clothing.AnimationSpriteSheetTotalRowJumps; i++)
        {
            if (currentIndex >= rowStartingIndexes.ElementAt(i) &&
                (currentIndex < rowStartingIndexes.ElementAt(i) + Clothing.AnimationFramesConnectedSize))
            {

                return rowStartingIndexes.ElementAt(i);
            }
        }
        
        // if not any of the other rows, then we should "wrap around" and restart
        return startingIndex;
    }
#endregion
    public static int GetNormalizedIndex(int startingIndex, int index, bool isHair)
    {
        //  0 to  7.
        //  80 to 87 -- 8 to 15
        // 160 to 167 -- 16 to 23
        // 240 to 247 -- 24 to 31
        int jumpValue = isHair ? Clothing.AnimationSpriteSheetJumpHair : Clothing.AnimationSpriteSheetJump;
        
        if (Mathf.Abs(index - startingIndex) < Clothing.AnimationFramesConnectedSize)
        {
            //this case its the first row with no jumps, so no modifications needed
            return Mathf.Abs(index - startingIndex);
            

        }
        else
        {
            List<int> rowStartingIndexes = new List<int>(); // we exclude the first row;
            for (int i = 0; i < Clothing.AnimationSpriteSheetTotalRowJumps; i++)
            {
                rowStartingIndexes.Add(startingIndex + Clothing.AnimationFramesConnectedSize + (jumpValue *(i + 1)) );
            }

            int currentRowStartingIndex = 0;
            int row = 0;
            
            for (int i = 0; i < Clothing.AnimationSpriteSheetTotalRowJumps; i++)
            {
                if (index >= rowStartingIndexes.ElementAt(i) &&
                    (index < rowStartingIndexes.ElementAt(i) + Clothing.AnimationFramesConnectedSize))
                {

                    currentRowStartingIndex = rowStartingIndexes.ElementAt(i);
                    row = i + 1;
                    break;
                }
            }
            
            return Mathf.Abs(index - currentRowStartingIndex) + (Clothing.AnimationFramesConnectedSize * row);
            
        }
        
    }
    
    public static int GetIndexFromNormalizedIndex(int startingIndex, int normalizedIndex, bool isHair)
    {
        //  0 to  7.
        //  80 to 87 -- 8 to 15
        // 160 to 167 -- 16 to 23
        // 240 to 247 -- 24 to 31
        int jumpValue = isHair ? Clothing.AnimationSpriteSheetJumpHair : Clothing.AnimationSpriteSheetJump;


        if (normalizedIndex < Clothing.AnimationFramesConnectedSize)
        {
            return startingIndex + normalizedIndex;
        }
        else
        {
            List<int> rowStartingIndexes = new List<int>(); // we exclude the first row;
            for (int i = 0; i < Clothing.AnimationSpriteSheetTotalRowJumps; i++)
            {
                rowStartingIndexes.Add(0 + Clothing.AnimationFramesConnectedSize + (Clothing.AnimationFramesConnectedSize *(i)) );
            }

            int currentRowStartingNormalizedIndex = 0;
            int row = 0;
            
            for (int i = 0; i < Clothing.AnimationSpriteSheetTotalRowJumps; i++)
            {
                if (normalizedIndex >= rowStartingIndexes.ElementAt(i) &&
                    (normalizedIndex < rowStartingIndexes.ElementAt(i) + Clothing.AnimationFramesConnectedSize))
                {

                    currentRowStartingNormalizedIndex = rowStartingIndexes.ElementAt(i);
                    row = i + 1;
                    break;
                }
            }

            return Mathf.Abs(normalizedIndex - currentRowStartingNormalizedIndex) + startingIndex + (jumpValue * (row) + ((row) * (Clothing.AnimationFramesConnectedSize-1)));
            
        }
        
    }
}
