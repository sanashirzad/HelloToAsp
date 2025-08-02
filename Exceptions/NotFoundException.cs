namespace HelloToAsp.Exceptions
{
    public class NotFoundException(string name, object key) : ApplicationException($"{name} {key} was not found")
    {
        //public NotFoundException(string name, object key) : base($"{name} {key} was not found")
        //{

        //}
        // I used a primary constructor
    }
}
