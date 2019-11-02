namespace ShoppingEcommerce.Errors
{
    public class FileNotFoundError : Error
    {
        private readonly string _filePath;

        /// <summary>
        /// </summary>
        /// <param name="filePath"></param>
        public FileNotFoundError(string filePath)
        {
            _filePath = filePath;
        }

        public override string Message => $"File not found: {_filePath}";
    }
}