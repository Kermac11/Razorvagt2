namespace Razorvagt2.Models
{
    public class Tag
    {
        private int _typeid;
        private string _type;

        public Tag()
        {

        }

        public Tag(int id, string type)
        {
            _typeid = id;
            _type = type;
        }

        public int ID
        {
            get { return _typeid; }
            set { _typeid = value; }
        }


        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }


    }
}
