using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class seg042
{
    private readonly KeyboardService _keyboardService;
    private readonly seg051 _seg051;
    public seg042(KeyboardService keyboardService, seg051 seg051)
    {
        _keyboardService = keyboardService;
        _seg051 = seg051;
    }

    private void debug_display(string text)
    {
        Logging.Logger.Log(text);
        _keyboardService.GetInputKey();
    }


    internal void delete_file(string fileString)
    {
        if (System.IO.File.Exists(fileString))
        {
            System.IO.File.Delete(fileString);
        }
    }



    internal bool find_and_open_file(out File file_ptr, bool noError, string full_file_name)
    {
        string file_name = System.IO.Path.GetFileName(full_file_name);
        string dir_path = System.IO.Path.GetDirectoryName(full_file_name);

        if (dir_path.Length == 0)
        {
            dir_path = gbl.exe_path;
        }

        bool file_found;

        file_found = System.IO.File.Exists(System.IO.Path.Combine(dir_path, file_name));

        if (file_found == false && noError == false)
        {
            debug_display("Couldn't find " + file_name + ". Check install.");
        }

        if (file_found == true)
        {
            file_ptr = new File();
            file_ptr.Assign(System.IO.Path.Combine(dir_path, file_name));

            _seg051.Reset(file_ptr);
        }
        else
        {
            file_ptr = null;
        }

        return file_found;
    }


    internal bool file_find(string filePath)
    {
        return System.IO.File.Exists(filePath);
    }


    private char[] unk_16FA9 = { ' ', '.', '*', ',', '?', '/', '\\', ':', ';', '|' };

    internal string clean_string(string s)
    {
        string cleanStr = s.Trim(unk_16FA9).ToLower();

        if (cleanStr.Length > 8)
        {
            cleanStr = cleanStr.Substring(0, 8);
        }

        return cleanStr;
    }


    private bool setupDaxFiles(out System.IO.BinaryReader fileA, out System.IO.BinaryReader fileB, out short arg_8, string file_name)
    {
        fileA = null;
        fileB = null;
        arg_8 = 0;

        if (System.IO.File.Exists(file_name) == false)
        {
            /*TODO Add message about missing file here.*/
            return false;
        }

        try
        {
            System.IO.FileStream fsA = new System.IO.FileStream(file_name, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            System.IO.FileStream fsB = new System.IO.FileStream(file_name, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);

            fileA = new System.IO.BinaryReader(fsA);
            fileB = new System.IO.BinaryReader(fsB);
        }
        catch (System.ApplicationException)
        {
            /*TODO Add message about error here.*/
            return false;
        }

        arg_8 = fileA.ReadInt16();
        arg_8 += 2;

        fileB.BaseStream.Seek(arg_8, System.IO.SeekOrigin.Begin);
        return true;
    }

    internal void set_game_area(byte arg_0)
    {
        gbl.game_area_backup = gbl.game_area;
        gbl.game_area = arg_0;
    }


    internal void restore_game_area()
    {
        gbl.game_area = gbl.game_area_backup;
    }
}
