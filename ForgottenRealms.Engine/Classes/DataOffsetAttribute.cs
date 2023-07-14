namespace ForgottenRealms.Engine.Classes;

public class DataOffsetAttribute : Attribute
{
    // The constructor is called when the attribute is set.
    public DataOffsetAttribute(int offset, DataType type)
    {
        this.offset = offset;
        this.type = type;
        size = DefaultSize(type);
    }

    public DataOffsetAttribute(int offset, DataType type, int size)
    {
        this.offset = offset;
        this.type = type;
        this.size = size;
    }

    private int DefaultSize(DataType type)
    {
        switch (type)
        {
            case DataType.Byte:
                return 1;
            case DataType.SByte:
                return 1;
            case DataType.IByte:
                return 1;
            case DataType.Bool:
                return 1;
            case DataType.Word:
                return 2;
            case DataType.SWord:
                return 2;
            case DataType.Int:
                return 4;
            default:
                throw new NotImplementedException();
        }
    }

    // Keep a variable internally ...
    protected int offset;
    public int Offset => offset;

    protected int size;
    public int Size => size;

    protected DataType type;
    public DataType Type => type;
}
