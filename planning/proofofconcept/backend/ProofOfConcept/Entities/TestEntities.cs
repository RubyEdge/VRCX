namespace ProofOfConcept.Entities
{
    public class TestMessage
    {
        public required string Message { get; set; }
    }

    public class TestResponse
    {
        public required string Message { get; set; }
    }

    public class ErrorResponse
    {
        public required string Error { get; set; }
    }
}
