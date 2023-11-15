namespace GifStorage.Exceptions;

public class ElementIsAlreadyExists : Exception {
	public ElementIsAlreadyExists(string message) : base(message) { }
	public ElementIsAlreadyExists() : base("Element is already exists exception") { }
}
