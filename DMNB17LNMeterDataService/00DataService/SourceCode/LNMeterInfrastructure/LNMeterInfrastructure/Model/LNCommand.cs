using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class LNCommand
    {
        private int _id = Int32.MinValue;
        private int _metercategoryid = Int32.MinValue;
        private string _commandName = null;
        private string _commandValue = null;

        /// <summary>
        ///         ''' Primary ID(NOT NULL)
        ///         ''' </summary>
        public Int32 ID
        {
            set
            {
                _id = value;
            }
            get
            {
                return _id;
            }
        }

        /// <summary>
        ///         ''' CommandType(NOT NULL)
        ///         ''' </summary>
        public int MeterCategoryID
        {
            set
            {
                _metercategoryid = value;
            }
            get
            {
                return _metercategoryid;
            }
        }

        /// <summary>
        ///         ''' CommandName(NOT NULL)
        ///         ''' </summary>
        public string CommandName
        {
            set
            {
                _commandName = value;
            }
            get
            {
                return _commandName;
            }
        }

        /// <summary>
        ///         ''' CommandValue(NOT NULL)
        ///         ''' </summary>
        public string CommandValue
        {
            set
            {
                _commandValue = value;
            }
            get
            {
                return _commandValue;
            }
        }
    }
}
