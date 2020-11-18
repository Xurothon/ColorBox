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

    private readonly TileType[, ] level4 = new TileType[, ]
    { { TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.TYPE2 },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK },

        { TileType.TYPE2, TileType.BLOCK, TileType.TYPE2, TileType.TYPE3 },

        { TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.PLAYER }
    };

    private readonly TileType[, ] level5 = new TileType[, ]
    { { TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.TYPE2 },

        { TileType.TYPE2, TileType.BLOCK, TileType.TYPE2, TileType.TYPE3 },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK },

        { TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.PLAYER }
    };

    private readonly TileType[, ] level6 = new TileType[, ]
    { { TileType.TYPE2, TileType.TYPE3, TileType.BLOCK, TileType.TYPE3 },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE1, TileType.TYPE1 },

        { TileType.TYPE1, TileType.BLOCK, TileType.TYPE3, TileType.TYPE2 },

        { TileType.TYPE3, TileType.TYPE2, TileType.BLOCK, TileType.PLAYER }
    };

    private readonly TileType[, ] level7 = new TileType[, ]
    { { TileType.TYPE1, TileType.BLOCK, TileType.TYPE3, TileType.TYPE2 },

        { TileType.TYPE2, TileType.TYPE3, TileType.BLOCK, TileType.TYPE3 },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE1, TileType.TYPE1 },

        { TileType.TYPE3, TileType.TYPE2, TileType.BLOCK, TileType.PLAYER }
    };

    private readonly TileType[, ] level8 = new TileType[, ]
    { { TileType.TYPE2, TileType.TYPE3, TileType.BLOCK, TileType.TYPE3 },

        { TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.TYPE3 },

        { TileType.TYPE1, TileType.BLOCK, TileType.TYPE3, TileType.TYPE2 },

        { TileType.TYPE3, TileType.BLOCK, TileType.BLOCK, TileType.PLAYER }
    };

    private readonly TileType[, ] level9 = new TileType[, ]
    { { TileType.TYPE1, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE3 },

        { TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.TYPE2, TileType.TYPE2 },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.BLOCK },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.TYPE3 },

        { TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.PLAYER }
    };

    private readonly TileType[, ] level10 = new TileType[, ]
    { { TileType.TYPE1, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE3 },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.TYPE3 },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.BLOCK },

        { TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.TYPE2, TileType.TYPE2 },

        { TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.PLAYER }
    };

    private readonly TileType[, ] level11 = new TileType[, ]
    { { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.TYPE3 },

        { TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.TYPE2, TileType.TYPE2 },

        { TileType.TYPE3, TileType.TYPE1, TileType.TYPE3, TileType.TYPE3, TileType.TYPE1 },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.BLOCK },

        { TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.PLAYER }
    };

    private readonly TileType[, ] level12 = new TileType[, ]
    { { TileType.TYPE3, TileType.TYPE2, TileType.BLOCK, TileType.TYPE3, TileType.BLOCK },

        { TileType.TYPE1, TileType.TYPE3, TileType.TYPE3, TileType.TYPE2, TileType.TYPE1 },

        { TileType.TYPE2, TileType.TYPE2, TileType.TYPE1, TileType.TYPE1, TileType.TYPE3 },

        { TileType.TYPE1, TileType.TYPE3, TileType.BLOCK, TileType.TYPE2, TileType.TYPE1 },

        { TileType.TYPE1, TileType.TYPE3, TileType.BLOCK, TileType.TYPE2, TileType.PLAYER }
    };

    private readonly TileType[, ] level13 = new TileType[, ]
    { { TileType.TYPE3, TileType.TYPE2, TileType.BLOCK, TileType.TYPE3, TileType.BLOCK },

        { TileType.TYPE1, TileType.TYPE3, TileType.BLOCK, TileType.TYPE2, TileType.TYPE1 },

        { TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1 },

        { TileType.TYPE2, TileType.TYPE2, TileType.BLOCK, TileType.TYPE1, TileType.TYPE3 },

        { TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.PLAYER }
    };

    private readonly TileType[, ] level14 = new TileType[, ]
    { { TileType.TYPE1, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE3, TileType.TYPE3 },

        { TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.BLOCK, TileType.TYPE2, TileType.TYPE2 },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.BLOCK, TileType.BLOCK },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3 },

        { TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.PLAYER, TileType.TYPE1 }
    };

    private readonly TileType[, ] level15 = new TileType[, ]
    { { TileType.TYPE3, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE1, TileType.TYPE3 },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3 },

        { TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.BLOCK, TileType.TYPE2, TileType.TYPE1 },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.BLOCK, TileType.BLOCK },

        { TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.PLAYER, TileType.TYPE2 }
    };

    private readonly TileType[, ] level16 = new TileType[, ]
    { { TileType.TYPE3, TileType.TYPE2, TileType.BLOCK, TileType.TYPE3, TileType.BLOCK, TileType.TYPE1 },

        { TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.TYPE2, TileType.TYPE2 },

        { TileType.TYPE2, TileType.TYPE1, TileType.BLOCK, TileType.BLOCK, TileType.BLOCK, TileType.TYPE1 },

        { TileType.TYPE2, TileType.BLOCK, TileType.TYPE1, TileType.TYPE2, TileType.TYPE3, TileType.TYPE2 },

        { TileType.TYPE3, TileType.TYPE2, TileType.TYPE2, TileType.TYPE1, TileType.PLAYER, TileType.TYPE1 }
    };

    private readonly TileType[, ] level17 = new TileType[, ]
    { { TileType.TYPE2, TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.TYPE3, TileType.TYPE2 },

        { TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.TYPE3, TileType.BLOCK, TileType.TYPE1 },

        { TileType.BLOCK, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.TYPE2, TileType.TYPE2 },

        { TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.BLOCK, TileType.TYPE1 },

        { TileType.TYPE2, TileType.TYPE2, TileType.BLOCK, TileType.BLOCK, TileType.PLAYER, TileType.TYPE1 }
    };

    private readonly TileType[, ] level18 = new TileType[, ]
    { { TileType.TYPE2, TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.TYPE3, TileType.TYPE2 },

        { TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.BLOCK, TileType.TYPE1 },

        { TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.TYPE3, TileType.BLOCK, TileType.TYPE1 },

        { TileType.BLOCK, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.TYPE2, TileType.TYPE2 },

        { TileType.TYPE2, TileType.TYPE2, TileType.BLOCK, TileType.BLOCK, TileType.PLAYER, TileType.TYPE1 }
    };

    private readonly TileType[, ] level19 = new TileType[, ]
    { { TileType.TYPE1, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE3, TileType.TYPE3 },

        { TileType.TYPE3, TileType.ENEMY, TileType.TYPE1, TileType.BLOCK, TileType.TYPE2, TileType.TYPE2 },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.BLOCK, TileType.BLOCK },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3 },

        { TileType.ENEMY, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.PLAYER, TileType.TYPE1 },
    };

    private readonly TileType[, ] level20 = new TileType[, ]
    { { TileType.TYPE1, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1 },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.ENEMY, TileType.TYPE3 },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.BLOCK, TileType.BLOCK },

        { TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.BLOCK, TileType.TYPE2, TileType.TYPE2 },

        { TileType.ENEMY, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.PLAYER, TileType.TYPE1 },
    };

    private readonly TileType[, ] level21 = new TileType[, ]
    { { TileType.TYPE2, TileType.TYPE2, TileType.BLOCK, TileType.TYPE3, TileType.BLOCK, TileType.TYPE1 },

        { TileType.TYPE3, TileType.TYPE3, TileType.ENEMY, TileType.TYPE1, TileType.TYPE2, TileType.TYPE2 },

        { TileType.TYPE1, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE3, TileType.BLOCK },

        { TileType.BLOCK, TileType.TYPE2, TileType.TYPE2, TileType.BLOCK, TileType.TYPE2, TileType.TYPE3 },

        { TileType.ENEMY, TileType.TYPE1, TileType.BLOCK, TileType.TYPE2, TileType.PLAYER, TileType.TYPE3 },
    };

    private readonly TileType[, ] level22 = new TileType[, ]
    { { TileType.TYPE1, TileType.TYPE2, TileType.TYPE2, TileType.TYPE1, TileType.TYPE2, TileType.TYPE3 },

        { TileType.TYPE2, TileType.ENEMY, TileType.BLOCK, TileType.TYPE3, TileType.BLOCK, TileType.BLOCK },

        { TileType.TYPE2, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1 },

        { TileType.TYPE3, TileType.TYPE3, TileType.ENEMY, TileType.TYPE1, TileType.TYPE2, TileType.TYPE2 },

        { TileType.BLOCK, TileType.BLOCK, TileType.BLOCK, TileType.TYPE2, TileType.PLAYER, TileType.TYPE3 },
    };

    private readonly TileType[, ] level23 = new TileType[, ]
    { { TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.BLOCK, TileType.TYPE2, TileType.TYPE2 },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.ENEMY, TileType.TYPE3 },

        { TileType.TYPE1, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1 },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.BLOCK, TileType.BLOCK },

        { TileType.ENEMY, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.PLAYER, TileType.TYPE1 },
    };

    private readonly TileType[, ] level24 = new TileType[, ]
    { { TileType.TYPE3, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.TYPE3, TileType.BLOCK },

        { TileType.BLOCK, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.TYPE3, TileType.BLOCK },

        { TileType.TYPE3, TileType.ENEMY, TileType.TYPE1, TileType.BLOCK, TileType.TYPE2, TileType.TYPE2, TileType.TYPE3 },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.TYPE3, TileType.BLOCK, TileType.BLOCK },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.TYPE2 },

        { TileType.ENEMY, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.PLAYER, TileType.TYPE1, TileType.ENEMY }
    };

    private readonly TileType[, ] level25 = new TileType[, ]
    { { TileType.TYPE3, TileType.ENEMY, TileType.TYPE1, TileType.BLOCK, TileType.TYPE2, TileType.TYPE2, TileType.TYPE3 },

        { TileType.TYPE3, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.TYPE3, TileType.TYPE3, TileType.BLOCK },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.TYPE2 },

        { TileType.BLOCK, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.TYPE3, TileType.BLOCK },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.TYPE3, TileType.BLOCK, TileType.BLOCK },

        { TileType.ENEMY, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.PLAYER, TileType.TYPE1, TileType.ENEMY }
    };

    private readonly TileType[, ] level26 = new TileType[, ]
    { { TileType.ENEMY, TileType.TYPE2, TileType.BLOCK, TileType.TYPE3, TileType.BLOCK, TileType.BLOCK, TileType.TYPE1 },

        { TileType.TYPE3, TileType.TYPE3, TileType.ENEMY, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.TYPE2 },

        { TileType.TYPE1, TileType.TYPE1, TileType.TYPE3, TileType.TYPE3, TileType.TYPE1, TileType.TYPE3, TileType.BLOCK },

        { TileType.BLOCK, TileType.BLOCK, TileType.TYPE1, TileType.TYPE2, TileType.TYPE2, TileType.TYPE3, TileType.TYPE2 },

        { TileType.BLOCK, TileType.TYPE2, TileType.TYPE1, TileType.TYPE3, TileType.BLOCK, TileType.TYPE2, TileType.TYPE3 },

        { TileType.TYPE3, TileType.ENEMY, TileType.TYPE2, TileType.BLOCK, TileType.PLAYER, TileType.TYPE3, TileType.BLOCK }
    };

    private readonly TileType[, ] level28 = new TileType[, ]
    { { TileType.TYPE2, TileType.TYPE3, TileType.TYPE1, TileType.TYPE2, TileType.TYPE2, TileType.TYPE3, TileType.TYPE2 },

        { TileType.BLOCK, TileType.TYPE2, TileType.TYPE1, TileType.TYPE3, TileType.BLOCK, TileType.TYPE2, TileType.TYPE3 },

        { TileType.TYPE3, TileType.TYPE3, TileType.ENEMY, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.BLOCK },

        { TileType.TYPE1, TileType.TYPE1, TileType.TYPE3, TileType.TYPE3, TileType.TYPE1, TileType.TYPE3, TileType.BLOCK },

        { TileType.ENEMY, TileType.TYPE2, TileType.BLOCK, TileType.BLOCK, TileType.TYPE3, TileType.TYPE2, TileType.TYPE1 },

        { TileType.TYPE3, TileType.ENEMY, TileType.TYPE2, TileType.BLOCK, TileType.PLAYER, TileType.TYPE3, TileType.BLOCK }
    };

    private readonly TileType[, ] level27 = new TileType[, ]
    { { TileType.TYPE3, TileType.TYPE1, TileType.TYPE1, TileType.BLOCK, TileType.TYPE2, TileType.TYPE2, TileType.TYPE3 },

        { TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.TYPE1, TileType.TYPE1, TileType.TYPE3, TileType.BLOCK },

        { TileType.BLOCK, TileType.TYPE3, TileType.TYPE1, TileType.BLOCK, TileType.TYPE3, TileType.BLOCK, TileType.BLOCK },

        { TileType.ENEMY, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.TYPE2, TileType.TYPE1, TileType.ENEMY },

        { TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE3, TileType.TYPE2, TileType.TYPE1, TileType.TYPE2 },

        { TileType.BLOCK, TileType.TYPE1, TileType.TYPE3, TileType.TYPE1, TileType.PLAYER, TileType.TYPE3, TileType.BLOCK }

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
                return level4;
            case 5:
                return level5;
            case 6:
                return level6;
            case 7:
                return level7;
            case 8:
                return level8;
            case 9:
                return level9;
            case 10:
                return level10;
            case 11:
                return level11;
            case 12:
                return level12;
            case 13:
                return level13;
            case 14:
                return level14;
            case 15:
                return level15;
            case 16:
                return level16;
            case 17:
                return level17;
            case 18:
                return level18;
            case 19:
                return level19;
            case 20:
                return level20;
            case 21:
                return level21;
            case 22:
                return level22;
            case 23:
                return level23;
            case 24:
                return level24;
            case 25:
                return level25;
            case 26:
                return level26;
            case 27:
                return level27;
            case 28:
                return level28;

        }
        return level1;
    }

}