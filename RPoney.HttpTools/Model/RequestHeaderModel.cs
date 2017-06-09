namespace RPoney.HttpTools.Model
{
    public class RequestHeaderModel
    {
        public string Method { get; set; }

        public string Url { get; set; }

        public string ContentType { get; set; }

        public string Charset { get; set; }

        public string UserAgent { get; set; }

        public string Param { get; set; }
    }
}
