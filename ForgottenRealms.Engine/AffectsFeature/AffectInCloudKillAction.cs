using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class AffectInCloudKillAction : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_in_cloud_kill;
    private readonly ovr025 _ovr025;
    public AffectInCloudKillAction(ovr025 ovr025)
    {
        _ovr025 = ovr025;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Affect affect = (Affect)param;

        GasCloud cell = gbl.CloudKillCloud.Find(c => c.player == player && c.field_1C == (affect.affect_data >> 4));

        if (cell != null)
        {
            _ovr025.string_print01("The air clears a little...");

            for (int var_B = 0; var_B < 9; var_B++)
            {
                if (cell.present[var_B] == true)
                {
                    var tmp = cell.targetPos + gbl.MapDirectionDelta[gbl.CloudDirections[var_B]];

                    bool var_E = gbl.downedPlayers.Exists(c => c.target != null && c.map == tmp);

                    if (var_E == true)
                    {
                        gbl.mapToBackGroundTile[tmp] = gbl.Tile_DownPlayer;
                    }
                    else
                    {
                        gbl.mapToBackGroundTile[tmp] = cell.groundTile[var_B];
                    }
                }
            }


            gbl.CloudKillCloud.Remove(cell);

            foreach (var var_4 in gbl.CloudKillCloud)
            {
                for (int var_B = 0; var_B < 9; var_B++)
                {
                    if (var_4.present[var_B] == true)
                    {
                        var tmp = var_4.targetPos + gbl.MapDirectionDelta[gbl.CloudDirections[var_B]];

                        gbl.mapToBackGroundTile[tmp] = gbl.Tile_CloudKill;
                    }
                }
            }
        }
    }
}
