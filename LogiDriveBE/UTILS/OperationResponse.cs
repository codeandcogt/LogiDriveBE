namespace LogiDriveBE.UTILS
{
    public class OperationResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public OperationResponse(int code, string message = "", T data = default)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }
}
