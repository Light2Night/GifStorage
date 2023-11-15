namespace GifStorage.Exceptions;

public class ElementIsNotExists : Exception {
	public ElementIsNotExists(string message) : base(message) { }
	public ElementIsNotExists() : base("Element is not exists exception") { }
}