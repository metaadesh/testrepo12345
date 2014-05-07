using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace METAOPTION.BAL
{  
   public class LaneSettings
    {
        string _year;

        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }
        string _make;

        public string Make
        {
            get { return _make; }
            set { _make = value; }
        }
        string _model;

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }
        string _laneType;

        public string LaneType
        {
            get { return _laneType; }
            set { _laneType = value; }
        }
        string _lane;

        public string Lane
        {
            get { return _lane; }
            set { _lane = value; }
        }

        string _sortKey1;

        public string SortKey1
        {
            get { return _sortKey1; }
            set { _sortKey1 = value; }
        }
        string _sortKey2;

        public string SortKey2
        {
            get { return _sortKey2; }
            set { _sortKey2 = value; }
        }
        string _sortKey3;

        public string SortKey3
        {
            get { return _sortKey3; }
            set { _sortKey3 = value; }
        }
        string _sortKey4;

        public string SortKey4
        {
            get { return _sortKey4; }
            set { _sortKey4 = value; }
        }
        string _sortKey5;

        public string SortKey5
        {
            get { return _sortKey5; }
            set { _sortKey5 = value; }
        }
        string _sortKey6;

        public string SortKey6
        {
            get { return _sortKey6; }
            set { _sortKey6 = value; }
        }

        string _sortKey1Order;

        public string SortKey1Order
        {
            get { return _sortKey1Order; }
            set { _sortKey1Order = value; }
        }
        string _sortKey2Order;

        public string SortKey2Order
        {
            get { return _sortKey2Order; }
            set { _sortKey2Order = value; }
        }
        string _sortKey3Order;

        public string SortKey3Order
        {
            get { return _sortKey3Order; }
            set { _sortKey3Order = value; }
        }
        string _sortKey4Order;

        public string SortKey4Order
        {
            get { return _sortKey4Order; }
            set { _sortKey4Order = value; }
        }
        string _sortKey5Order;

        public string SortKey5Order
        {
            get { return _sortKey5Order; }
            set { _sortKey5Order = value; }
        }
        string _sortKey6Order;

        public string SortKey6Order
        {
            get { return _sortKey6Order; }
            set { _sortKey6Order = value; }
        }

        string _exoticPattern;

        public string ExoticPattern
        {
            get { return _exoticPattern; }
            set { _exoticPattern = value; }
        }
        int _viewCount;

        public int ViewCount
        {
            get { return _viewCount; }
            set { _viewCount = value; }
        }
        bool _isShowExotic;

        public bool IsShowExotic
        {
            get { return _isShowExotic; }
            set { _isShowExotic = value; } 
        }
        bool _isShowOnlineandVirtual;

        public bool IsShowOnlineandVirtual
        {
            get { return _isShowOnlineandVirtual; }
            set { _isShowOnlineandVirtual = value; }
        }

        int _gridPagePosition;

        public int gridPagePosition
        {
          get { return _gridPagePosition; }
          set { _gridPagePosition = value; }
        }

    }
}
