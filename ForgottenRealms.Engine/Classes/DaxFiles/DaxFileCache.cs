﻿using System;
using System.Collections.Generic;
using System.IO;

namespace ForgottenRealms.Engine.Classes.DaxFiles;

public class DaxFileCache
{
    private Dictionary<int, byte[]> entries;

    internal DaxFileCache(string filename)
    {
        entries = new Dictionary<int, byte[]>();

        LoadFile(filename);
    }

    private void LoadFile(string filename)
    {
        var fileInfo = GameFileLoader.GetFileInfo(filename);
        var dataOffset = 0;

        if (fileInfo.Exists == false)
        {
            return;
        }

        BinaryReader fileA;

        try
        {
            var fsA = new FileStream(fileInfo.FullName, FileMode.Open,
                FileAccess.Read, FileShare.Read);

            fileA = new BinaryReader(fsA);
        }
        catch (ApplicationException)
        {
            return;
        }

        dataOffset = fileA.ReadInt16() + 2;

        List<DaxHeaderEntry> headers = new();

        const int headerEntrySize = 9;

        for (var i = 0; i < (dataOffset - 2) / headerEntrySize; i++)
        {
            var dhe = new DaxHeaderEntry();
            dhe.id = fileA.ReadByte();
            dhe.offset = fileA.ReadInt32();
            dhe.rawSize = fileA.ReadInt16();
            dhe.compSize = fileA.ReadUInt16();

            headers.Add(dhe);
        }

        foreach (var dhe in headers)
        {
            var comp = new byte[dhe.compSize];
            var raw = new byte[dhe.rawSize];

            fileA.BaseStream.Seek(dataOffset + dhe.offset, SeekOrigin.Begin);

            comp = fileA.ReadBytes(dhe.compSize);

            Decode(dhe.rawSize, dhe.compSize, raw, comp);

            entries.Add(dhe.id, raw);
        }

        fileA.Close();
    }

    private void Decode(int decodeSize, int dataLength, byte[] output_ptr, byte[] input_ptr)
    {
        sbyte run_length;
        int output_index;
        int input_index;

        input_index = 0;
        output_index = 0;

        do
        {
            run_length = (sbyte)input_ptr[input_index];

            if (run_length >= 0)
            {
                for (var i = 0; i <= run_length; i++)
                {
                    output_ptr[output_index + i] = input_ptr[input_index + i + 1];
                }

                input_index += run_length + 2;
                output_index += run_length + 1;
            }
            else
            {
                run_length = (sbyte)-run_length;

                for (var i = 0; i < run_length; i++)
                {
                    output_ptr[output_index + i] = input_ptr[input_index + 1];
                }

                input_index += 2;
                output_index += run_length;
            }
        } while (input_index < dataLength);
    }

    internal byte[] GetData(int block_id)
    {
        byte[] orig;
        if (entries.TryGetValue(block_id, out orig) == false)
        {
            return null;
        }

        return (byte[])orig.Clone();
    }
}
