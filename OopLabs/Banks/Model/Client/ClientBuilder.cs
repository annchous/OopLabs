namespace Banks.Model.Client
{
    public class ClientBuilder
    {
        private Model.Client.Client _client = new Model.Client.Client();

        public ClientBuilder() => Reset();

        public ClientBuilder SetName(string name)
        {
            _client.Name = name;
            return this;
        }

        public ClientBuilder SetSurname(string surname)
        {
            _client.Surname = surname;
            return this;
        }

        public ClientBuilder SetAddress(string address)
        {
            _client.Address = address;
            return this;
        }

        public ClientBuilder SetPassport(string passport)
        {
            _client.Passport = passport;
            return this;
        }

        private void Reset() => _client = new Model.Client.Client();

        public Model.Client.Client GetClient() => _client;

        public static implicit operator Model.Client.Client(ClientBuilder clientBuilder) => clientBuilder._client;
    }
}