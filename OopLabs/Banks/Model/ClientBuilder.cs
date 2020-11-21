namespace Banks.Model
{
    public class ClientBuilder
    {
        private Client _client = new Client();

        public ClientBuilder() => this.Reset();

        public ClientBuilder SetName(string name)
        {
            this._client.Name = name;
            return this;
        }

        public ClientBuilder SetSurname(string surname)
        {
            this._client.Surname = surname;
            return this;
        }

        public ClientBuilder SetAddress(string address)
        {
            this._client.Address = address;
            return this;
        }

        public ClientBuilder SetPassport(string passport)
        {
            this._client.Passport = passport;
            return this;
        }

        private void Reset() => this._client = new Client();

        public Client GetClient() => this._client;

        public static implicit operator Client(ClientBuilder clientBuilder) => clientBuilder._client;
    }
}