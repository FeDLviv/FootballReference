using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.ComponentModel;
using System.Drawing;

namespace Football
{
    //(Name="FirstName")
    [DataContract]
    public class Player
    {
        private readonly DateTime MIN_DATE = new DateTime(1980, 1, 1);
        private readonly DateTime MAX_DATE = DateTime.Today;

        private string lastName;

        [DataMember]
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
            }
        }

        private string firstName;

        [DataMember]
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
            }
        }

        private DateTime birthday;

        [DataMember]
        public DateTime Birthday
        {
            get { return birthday; }
            set
            {
                {
                    birthday = value;
                }
            }
        }

        private Positions position;

        [DataMember]
        public Positions Position 
        {
            get { return position; }
            set 
            { 
                position = value; 
            }
        }

        private string club;

        [DataMember]
        public string Club 
        {
            get { return club; }
            set 
            { 
                club = value; 
            }
        }

        private int mathcs;

        [DataMember]
        public int Mathcs
        {
            get { return mathcs; }
            set
            {
                if (value >= 0)
                {
                    mathcs = value;
                }
            }
        }

        private int gools;

        [DataMember]
        public int Gools 
        {
            get { return gools; } 
            set
            {
                if (value >= 0)
                {
                    gools = value;
                }
            }
        }

        private byte[] imageByteArray;

        [DataMember]
        public byte[] ImageByteArray
        {
            get { return imageByteArray; }
            set { imageByteArray = value; }
        }

        private DateTime lastEdit;

        [DataMember]
        public DateTime LastEdit
        {
            get { return lastEdit; }
            set { lastEdit = value; }
        }

        public Player()
        {
            Birthday = MIN_DATE;
            ImageConverter converter = new ImageConverter();
            ImageByteArray = (byte[]) converter.ConvertTo(Properties.Resources._default, typeof(byte[]));
            LastEdit = DateTime.Now;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        } 
    }

    //[DataContract]
    public enum Positions
    {
        [Description("голкіпер")]
        //[EnumMember]
        GK,
        [Description("захисник")]
        //[EnumMember]
        DF,
        [Description("півзахисник")]
        //[EnumMember]
        MF,
        [Description("нападник")]
        //[EnumMember]
        FW
    }
}