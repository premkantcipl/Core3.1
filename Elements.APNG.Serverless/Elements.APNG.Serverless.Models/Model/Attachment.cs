namespace Elements.APNG.Serverless.Models.Model
{
    public class Attachment
    {
        public byte[] FileContent { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string ContentType { get; set; }
    }
}
