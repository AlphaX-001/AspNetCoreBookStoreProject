namespace BookProject.Models
{
    //For Email Addresses and mail Bodies
    public class UserEmailOptions
    {
        public List<string> EmailAddresses { get; set; }
        public string Subject { get; set; }
        public string  Body { get; set; }

        public List<KeyValuePair<string, string>> Placeholders { get; set; }
    }
}
