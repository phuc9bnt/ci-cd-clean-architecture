namespace CoreBusiness.Config
{
    public class Configuration
    {
        private static Configuration _instance;
        public string JWTKey { get; set; }

        private Configuration()
        {

        }

        public static Configuration GetInstance()
        {
            if(_instance == null)
            {
                _instance = new Configuration();
            }
            return _instance;
        }
    }
}
