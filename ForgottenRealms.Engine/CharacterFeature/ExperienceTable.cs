using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CharacterFeature;

public class ExperienceTable
{
    private const int IsNotAllowed = -1;

    /// <summary>
    /// an experience table based on class and level
    /// </summary>
    private static int[,] exp_table =
    {
/* Cleric */    { 0, 1501, 3001,  6001, 13001, 27501, 55001, 110001, 225001, 450001, IsNotAllowed, IsNotAllowed, IsNotAllowed },
/* Druid */     { 0, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed },
/* Fighter */   { 0, 2001, 4001,  8001, 18001, 35001, 70001, 125001, 250001, 500001,  750001, 1000001, IsNotAllowed  },
/* Paladin */   { 0, 2751, 5501, 12001, 24001, 45001, 95001, 175001, 350001, 700001, 1050001, IsNotAllowed, IsNotAllowed },
/* Ranger */    { 0, 2251, 4501, 10001, 20001, 40001, 90001, 150001, 225001, 325001,  650001, IsNotAllowed, IsNotAllowed },
/* MU */        { 0, 2501, 5001, 10001, 22501, 40001, 60001,  90001, 135001, 250001,  375001, IsNotAllowed, IsNotAllowed },
/* Thief */     { 0, 1251, 2501,  5001, 10001, 20001, 42501,  70001, 110001, 160001,  220001, 440001, IsNotAllowed},
/* Monk */      { 0, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed, IsNotAllowed }
    };

    public bool IsTrainingAllowed(ClassId _class, int class_lvl)
    {
        return GetMinimumExperience(_class, class_lvl) > 0;
    }

    public int GetMinimumExperience(ClassId _class, int class_lvl) => exp_table[(int)_class, class_lvl];

    public bool HasEnoughExperienceToTrain(ClassId _class, int class_lvl, Player player)
    {
        return exp_table[(int)_class, class_lvl] <= player.exp;
    }
}
