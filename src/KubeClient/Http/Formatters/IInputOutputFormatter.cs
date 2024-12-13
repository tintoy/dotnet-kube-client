namespace KubeClient.Http.Formatters
{
	/// <summary>
	///		Represents a formatter that can both serialise and deserialise data.
	/// </summary>
    public interface IInputOutputFormatter
		: IInputFormatter, IOutputFormatter
    {
    }
}
