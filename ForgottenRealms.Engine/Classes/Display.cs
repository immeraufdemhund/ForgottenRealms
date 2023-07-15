using System;

namespace ForgottenRealms.Engine.Classes;

public class Display
{
    private static byte[,] OrigEgaColors =
    {
        { 0, 0, 0 }, { 0, 0, 173 }, { 0, 173, 0 }, { 0, 173, 173 }, { 173, 0, 0 }, { 173, 0, 173 }, { 173, 82, 0 },
        { 173, 173, 173 }, { 82, 82, 82 }, { 82, 82, 255 }, { 82, 255, 82 }, { 82, 255, 255 }, { 255, 82, 82 },
        { 255, 82, 255 }, { 255, 255, 82 }, { 255, 255, 255 },
    };

    private static byte[,] egaColors =
    {
        { 0, 0, 0 }, { 0, 0, 173 }, { 0, 173, 0 }, { 0, 173, 173 }, { 173, 0, 0 }, { 173, 0, 173 }, { 173, 82, 0 },
        { 173, 173, 173 }, { 82, 82, 82 }, { 82, 82, 255 }, { 82, 255, 82 }, { 82, 255, 255 }, { 255, 82, 82 },
        { 255, 82, 255 }, { 255, 255, 82 }, { 255, 255, 255 },
    };

    private static int[,] ram;
    private static byte[] videoRam;
    private static byte[] videoRamBkUp;
    private static int videoRamSize;
    private static int scanLineWidth;
    private static int outputWidth;
    private static int outputHeight;

    public static byte[] VideoRam => videoRam;
    public delegate void VoidDeledate();

    private static VoidDeledate updateCallback;

    public static VoidDeledate UpdateCallback
    {
        set => updateCallback = value;
    }

    static Display()
    {
        outputHeight = 200;
        outputWidth = 320;
        ram = new int[outputHeight, outputWidth];
        scanLineWidth = outputWidth * 3;
        videoRamSize = scanLineWidth * outputHeight;
        videoRam = new byte[videoRamSize];
    }

    private static int[] MonoBitMask = { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };

    public static void DisplayMono8x8(int xCol, int yCol, byte[] monoData8x8, int bgColor, int fgColor)
    {
        var pX = xCol * 8;

        for (var yStep = 0; yStep < 8; yStep++)
        {
            var pY = yCol * 8 + yStep;
            int value = gbl.monoCharData[yStep];

            for (var i = 0; i < 8; i++)
            {
                ram[pY, pX + i] = (value & MonoBitMask[i]) != 0 ? fgColor : bgColor;
                SetVidPixel(pX + i, pY, ram[pY, pX + i]);
            }
        }
    }

    public static void SetEgaPalette(int index, int colour)
    {
        egaColors[index, 0] = OrigEgaColors[colour, 0];
        egaColors[index, 1] = OrigEgaColors[colour, 1];
        egaColors[index, 2] = OrigEgaColors[colour, 2];

        for (var y = 0; y < outputHeight; y++)
        {
            var vy = y * scanLineWidth;
            for (var x = 0; x < outputWidth; x++)
            {
                var vx = x * 3;
                var egaColor = ram[y, x];

                videoRam[vy + vx + 0] = egaColors[egaColor, 2];
                videoRam[vy + vx + 1] = egaColors[egaColor, 1];
                videoRam[vy + vx + 2] = egaColors[egaColor, 0];
            }
        }

        Update();
    }

    private static void SetVidPixel(int x, int y, int egaColor)
    {
        videoRam[y * scanLineWidth + x * 3 + 0] = egaColors[egaColor, 2];
        videoRam[y * scanLineWidth + x * 3 + 1] = egaColors[egaColor, 1];
        videoRam[y * scanLineWidth + x * 3 + 2] = egaColors[egaColor, 0];
    }

    private static int noUpdateCount;

    public static void UpdateStop() => noUpdateCount++;

    public static void UpdateStart()
    {
        noUpdateCount--;
        Update();
    }

    public static void Update()
    {
        if (noUpdateCount == 0)
        {
            RawCopy(videoRam, videoRamSize);

            if (updateCallback != null)
            {
                updateCallback.Invoke();
            }
        }
    }

    public static void ForceUpdate()
    {
        RawCopy(videoRam, videoRamSize);

        if (updateCallback != null)
        {
            updateCallback.Invoke();
        }
    }

    public static void SaveVidRam() => videoRamBkUp = (byte[])videoRam.Clone();

    public static void RestoreVidRam() => videoRam = videoRamBkUp;

    public static byte GetPixel(int x, int y) => (byte)ram[y, x];

    public static void SetPixel3(int x, int y, int value)
    {
        if (value < 16)
        {
            ram[y, x] = value;

            SetVidPixel(x, y, ram[y, x]);
        }

        if (value > 16) { }
    }


    public static void RawCopy(byte[] rawVideoRam, int rawVideoRamSize)
    {
        lock (videoRam)
        {
            Array.Copy(rawVideoRam, 0, videoRam, 0, rawVideoRamSize);
        }
    }
}
