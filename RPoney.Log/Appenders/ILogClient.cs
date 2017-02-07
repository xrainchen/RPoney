namespace RPoney.Log.Appenders
{
    internal interface ILogClient
    {
        // Methods
        void AddValue(LogModel value);
        void AddValues(LogModel[] values);
    }





}
