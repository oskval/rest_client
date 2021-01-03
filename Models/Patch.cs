namespace Client.Models
{
    public class Patch
    {
        public string op { get; set; }
        public string path { get; set; }
        public string value { get; set; }
    }
}