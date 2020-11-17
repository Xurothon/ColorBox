public class LevelTileArrays
{
    private readonly TileType[, ] level1 = new TileType[, ]
    { { TileType.TYPE2, TileType.TYPE3, TileType.TYPE1 },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2 },

        { TileType.TYPE1, TileType.TYPE1, TileType.PLAYER }
    };

    private readonly TileType[, ] level2 = new TileType[, ]
    { { TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.TYPE1 },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE1, TileType.TYPE2 },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3 },

        { TileType.TYPE1, TileType.TYPE1, TileType.TYPE2, TileType.PLAYER }
    };

    private readonly TileType[, ] level3 = new TileType[, ]
    { { TileType.TYPE3, TileType.TYPE2, TileType.TYPE2, TileType.TYPE1 },

        { TileType.TYPE2, TileType.BLOCK, TileType.TYPE1, TileType.TYPE2 },

        { TileType.TYPE2, TileType.TYPE3, TileType.BLOCK, TileType.TYPE3 },

        { TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.PLAYER }
    };

    private readonly TileType[, ] level4_8 = new TileType[, ]
    { { TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.TYPE2 },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK },

        { TileType.TYPE2, TileType.BLOCK, TileType.TYPE2, TileType.TYPE3 },

        { TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.PLAYER }
    };

    private readonly TileType[, ] level9_13 = new TileType[, ]
    { { TileType.TYPE1, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE3 },

        { TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.TYPE2, TileType.TYPE2 },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.BLOCK },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.TYPE3 },

        { TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.PLAYER }
    };

    private readonly TileType[, ] level14_18 = new TileType[, ]
    { { TileType.TYPE1, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE3, TileType.TYPE3 },

        { TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.BLOCK, TileType.TYPE2, TileType.TYPE2 },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.BLOCK, TileType.BLOCK },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3 },

        { TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.PLAYER, TileType.TYPE1 }
    };

    private readonly TileType[, ] level19_23 = new TileType[, ]
    { { TileType.TYPE1, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE3, TileType.TYPE3 },

        { TileType.TYPE3, TileType.ENEMY, TileType.TYPE1, TileType.BLOCK, TileType.TYPE2, TileType.TYPE2 },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.BLOCK, TileType.BLOCK },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3 },

        { TileType.ENEMY, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.PLAYER, TileType.TYPE1 },
    };

    private readonly TileType[, ] level24_28 = new TileType[, ]
    { { TileType.TYPE3, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.TYPE3, TileType.BLOCK },

        { TileType.BLOCK, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.TYPE3, TileType.BLOCK },

        { TileType.TYPE3, TileType.ENEMY, TileType.TYPE1, TileType.BLOCK, TileType.TYPE2, TileType.TYPE2, TileType.TYPE3 },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.TYPE3, TileType.BLOCK, TileType.BLOCK },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.TYPE2 },

        { TileType.ENEMY, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.PLAYER, TileType.TYPE1, TileType.ENEMY }
    };

    public TileType[, ] GetLevelArray (int index)
    {
        switch (index)
        {
            case 1:
                return level1;
            case 2:
                return level2;
            case 3:
                return level3;
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
                return level4_8;
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
                return level9_13;
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
                return level14_18;
            case 19:
            case 20:
            case 21:
            case 22:
            case 23:
                return level19_23;
            case 24:
            case 25:
            case 26:
            case 27:
            case 28:
                return level24_28;

        }
        return level1;
    }

}