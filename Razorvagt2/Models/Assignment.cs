namespace Razorvagt2.Models
{
    public class Assignment
    {

        private User _user;
        private int _assigmentId;
        private DateTime _date;
        private int _length;
        private string _assignmenType;
        private Schedule _schedule;

        public Assignment()
        {

        }
        public Assignment( int id, User user, DateTime date, int length, string assignmentType, Schedule schedule)
        {
            _user = user;
            _assigmentId = id;
            _date = date;
            _length = length;
            _assignmenType = assignmentType;
            _schedule = schedule;
        }


        public int ID
        {
            get { return _assigmentId; }
            set { _assigmentId = value; }
        }
     
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        
        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }

       

        public string AssignmentType
        {
            get { return _assignmenType; }
            set { _assignmenType = value; }
        }

      
        public Schedule Schedule
        {
            get { return _schedule; }
            set { _schedule = value; }
        }


        public User  User
        {
            get { return _user; }
            set { _user = value; }
        }

        


    }
}
