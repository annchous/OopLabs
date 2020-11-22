namespace Banks.Model
{
    public class ClientBuilder
    {
        private Client _client = new Client();

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

        private void Reset() => _client = new Client();

        public Client GetClient() => _client;

        public static implicit operator Client(ClientBuilder clientBuilder) => clientBuilder._client;
    }
}