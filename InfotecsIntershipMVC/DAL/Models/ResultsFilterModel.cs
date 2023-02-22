namespace InfotecsIntershipMVC.DAL.Models
{
    public class ResultsFilterModel
    {
        private bool _isFilenameAscSort = false;
        private bool _isFilenameDescSort = false;
        public bool IsFilenameAscSort 
        { 
            get { return _isFilenameAscSort; }
            set
            {
                if (value == true)
                {
                    if (_isFilenameDescSort == true)
                        _isFilenameDescSort = false;
                }

                _isFilenameAscSort = value;
            }
        }
        public bool IsFilenameDescSort
        {
            get { return _isFilenameDescSort; }
            set
            {
                if (value == true)
                {
                    if (_isFilenameAscSort == true)
                        _isFilenameAscSort = false;
                }

                _isFilenameDescSort = value;
            }
        }

        public DateTime FirstOperaionStart  { get; set; }
        public DateTime FirstOperaionEnd    { get; set; }

        public int AverageDuraionStart      { get; set; }
        public int AverageDuraionEnd        { get; set; }

        public float AverageValueStart      { get; set; }
        public float AverageValueEnd        { get; set; }
    }
}
