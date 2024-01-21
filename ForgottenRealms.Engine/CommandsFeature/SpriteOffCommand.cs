using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class SpriteOffCommand : IGameCommand
{
    public void Execute()
    {
        gbl.ecl_offset++;
        if (gbl.displayPlayerSprite)
        {
            gbl.can_draw_bigpic = true;
            ovr029.RedrawView();
            gbl.displayPlayerSprite = false;
            gbl.spriteChanged = false;
        }
    }
}
