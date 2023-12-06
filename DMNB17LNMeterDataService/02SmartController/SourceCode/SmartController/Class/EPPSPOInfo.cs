using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EPPSPOInfo
{
    string _LineName = string.Empty;
    public string LineName
    {
        get { return _LineName; }
        set { _LineName = value; }
    }
    string _PlanDate = string.Empty;
    public string PlanDate
    {
        get { return _PlanDate; }
        set { _PlanDate = value; }
    }
    int _PlanQty = 0;
    public int PlanQty
    {
        get { return _PlanQty; }
        set { _PlanQty = value; }
    }
    string _WO = string.Empty;
    public string WO
    {
        get { return _WO; }
        set { _WO = value; }
    }
    string _PN = string.Empty;
    public string PN
    {
        get { return _PN; }
        set { _PN = value; }
    }
    int _WOQty = 0;
    public int WOQty
    {
        get { return _WOQty; }
        set { _WOQty = value; }
    }
    string _starttime = string.Empty;
    public string StartTime
    {
        get { return _starttime; }
        set { _starttime = value; }
    }
    string _endtime = string.Empty;
    public string EndTime
    {
        get { return _endtime; }
        set { _endtime = value; }
    }
}

public class LNPOInfoCollection : LNCollectionBase
{
    public EPPSPOInfo Add(string key, EPPSPOInfo value)
    {
        return (EPPSPOInfo)base.Add(key, value);
    }


    public EPPSPOInfo Add(EPPSPOInfo value)
    {
        return (EPPSPOInfo)base.Add(value);
    }
}

