namespace InfotecsIntershipMVC.DAL.Models
{
    public class RecordsFilterModel
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

        public string ContainsPattern { get; set; }
        public string StartWithPattern { get; set; }
        public string FileName { get; set; }
    }
}
