namespace Razorvagt2.Models
{
    public class User
    {
        private int _userId;
        private string _name;
        private string _userName;
        private string _password;
        private string _phone;
        private string _email;
        private bool _admin;
        public User()
        {

        }

        public User(int id, string name,string username,string password, string phone, string email, bool admin )
        {
            _userId = id;
            _name = name;
            _userName = username;
            _password = password;
            _phone = phone;
            _email = email;
            _admin = admin;
        }

        public int ID
        {
            get { return _userId; }
            set { _userId = value; }
        }
       

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        public string Username
        {
            get { return _userName; }
            set { _userName = value; }
        }
      
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

       
        public string EMail
        {
            get { return _email; }
            set { _email = value; }
        }

        

        public  bool Admin
        {
            get { return _admin; }
            set { _admin = value; }
        }








    }
}
