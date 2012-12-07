namespace TestFlightUploader
{
    public class TestFlightPackage
    {
        private readonly string m_ApiToken;
        private readonly string m_TeamToken;
        private readonly string m_Notes;
        private readonly bool m_Notify;
        private readonly bool m_Replace;
        private readonly string m_DistributionLists;

        public TestFlightPackage(string apiToken, string teamToken, string file, string notes, bool notify, bool replace, string distributionLists)
        {
            m_ApiToken = apiToken;
            m_TeamToken = teamToken;
            File = file;
            m_Notes = notes;
            m_Notify = notify;
            m_Replace = replace;
            m_DistributionLists = distributionLists;
        }


        public string ApiToken
        {
            get { return m_ApiToken; }
        }

        public string TeamToken
        {
            get { return m_TeamToken; }
        }

        public string File { get; set; }

        public string Notes
        {
            get { return m_Notes; }
        }

        public bool Notify
        {
            get { return m_Notify; }
        }

        public bool Replace
        {
            get { return m_Replace; }
        }

        public string DistributionLists
        {
            get { return m_DistributionLists; }
        }
    }
}