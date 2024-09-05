namespace Blogedium_api.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(){}
        public NotFoundException(string message) : base(message){}
        public NotFoundException(string message , Exception innerException) : base(message, innerException){}
    }
}

// purpose of 3 constructore usage for custome Exceptions
// 1: 
// Purpose: This constructor initializes an instance of the NotFoundException class without any specific error 
// message or inner exception.
// Use Case: It allows you to throw a generic NotFoundException when a detailed error message or additional inner 
// exception details are not needed or available.

// 2: 
// Purpose: This constructor initializes an instance of the NotFoundException class with a specified error message.
// Use Case: It allows you to throw a NotFoundException with a custom error message that provides context about what 
// went wrong. This message can be useful for debugging or logging purposes.

// 3:
// Purpose: This constructor initializes an instance of the NotFoundException class with a specified error message 
// and a reference to the inner exception that is the cause of this exception.
// Use Case: It allows you to propagate the details of another exception (inner exception) that led to the
// NotFoundException. This is useful when you need to understand the chain of exceptions or errors that occurred.