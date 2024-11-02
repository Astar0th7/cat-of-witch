namespace Services.SaveLoad
{
    public interface IWriteListener : IReadListener
    {
        void Write(ProgressData data);
    }
}