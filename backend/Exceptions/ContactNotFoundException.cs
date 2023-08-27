namespace backend.Exceptions
{
    public class ContactNotFoundException : Exception
    {
        public ContactNotFoundException(int id) : base($"Contact with '{id}' not found.")
        {
        }
    }
}
