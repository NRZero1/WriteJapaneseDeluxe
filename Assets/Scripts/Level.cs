using Random = System.Random;
using System.Linq;

public class Level
{
    public int[] level = new int[3] {1, 2, 3};
    public int levelCount = 0;

    public void saveLevel()
    {
        if (SaveSystem.checkSaveFile())
        {
            SaveSystem.delete();
        }
        SaveSystem.save(this);
    }

    public void loadLevel()
    {
        LevelData levelData = SaveSystem.load();

        level = levelData.level;
        levelCount = levelData.levelCount;
    }

    public void shuffleLevel()
    {
        Random random = new Random();
        level = level.OrderBy(Int => random.Next()).ToArray();
    }
}
