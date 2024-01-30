using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class AffectInStinkingCloudAction : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_in_stinking_cloud;

    private readonly ovr025 _ovr025;
    public AffectInStinkingCloudAction(ovr025 ovr025)
    {
        _ovr025 = ovr025;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Affect affect = (Affect)param;

        var var_8 = gbl.StinkingCloud.Find(cell => cell.player == player && cell.field_1C == (affect.affect_data >> 4));

        if (var_8 != null)
        {
            _ovr025.string_print01("The air clears a little...");

            for (int var_B = 0; var_B < 4; var_B++)
            {
                if (var_8.present[var_B] == true)
                {
                    var tmp = var_8.targetPos + gbl.MapDirectionDelta[gbl.SmallCloudDirections[var_B]];

                    bool var_9 = gbl.downedPlayers.Exists(cell => cell.target != null && cell.map == tmp);

                    if (var_9 == true)
                    {
                        gbl.mapToBackGroundTile[tmp] = gbl.Tile_DownPlayer;
                    }
                    else
                    {
                        gbl.mapToBackGroundTile[tmp] = var_8.groundTile[var_B];
                    }
                }
            }

            gbl.StinkingCloud.Remove(var_8);

            foreach (var var_4 in gbl.StinkingCloud)
            {
                for (int var_B = 0; var_B < 4; var_B++)
                {
                    if (var_4.present[var_B] == true)
                    {
                        var tmp = gbl.MapDirectionDelta[gbl.SmallCloudDirections[var_B]] + var_4.targetPos;

                        gbl.mapToBackGroundTile[tmp] = gbl.Tile_StinkingCloud;
                    }
                }
            }
        }
    }
}
