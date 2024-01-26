using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class SpriteOffCommand : IGameCommand
{
    private readonly ovr029 _ovr029;
    public SpriteOffCommand(ovr029 ovr029)
    {
        _ovr029 = ovr029;
    }

    public void Execute()
    {
        gbl.ecl_offset++;
        if (gbl.displayPlayerSprite)
        {
            gbl.can_draw_bigpic = true;
            _ovr029.RedrawView();
            gbl.displayPlayerSprite = false;
            gbl.spriteChanged = false;
        }
    }
}
