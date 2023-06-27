namespace Razorvagt2.Models
{

    public class Schedule
    {
        private int _id;
        private DateTime _startDate;
        private DateTime _endDate;

        public Schedule()
        {

        }
        public Schedule(int id, DateTime startDate, DateTime endDate)
        {
            _id = id;
            _startDate = startDate;
            _endDate = endDate;
        }
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

    }
}
