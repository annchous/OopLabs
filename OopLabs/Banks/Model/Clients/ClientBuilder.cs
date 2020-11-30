using System;

namespace Banks.Model.Clients
{
    public class ClientBuilder
    {
        private String _name;
        private String _surname;
        private String _address;
        private String _passport;

        public ClientBuilder SetName(String name)
        {
            _name = name;
            return this;
        }

        public ClientBuilder SetSurname(String surname)
        {
            _surname = surname;
            return this;
        }

        public ClientBuilder SetAddress(String address)
        {
            _address = address;
            return this;
        }

        public ClientBuilder SetPassport(String passport)
        {
            _passport = passport;
            return this;
        }
        public Client GetClient() => new Client(_name, _surname, _address, _passport);
    }
}