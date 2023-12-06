using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using Microsoft.VisualBasic;

public class LNXMLDetailBuilder
{
    private class XMLElement
    {
        public SortedList m_Attributes;
        public SortedList m_Elements;

        public SortedList Attributes
        {
            get
            {
                if (this.m_Attributes == null)
                    this.m_Attributes = new SortedList();
                return this.m_Attributes;
            }
        }

        public SortedList Elements
        {
            get
            {
                if (this.m_Elements == null)
                    this.m_Elements = new SortedList();
                return this.m_Elements;
            }
        }
    }

    private XMLElement m_ht;  // Current pointer
    private ArrayList m_Records = new ArrayList();
    private XMLElement m_DefHT = new XMLElement();

    private bool m_EncodeXMLToUTF16 = false;

    public LNXMLDetailBuilder()
    {
        m_ht = m_DefHT;
    }

    public LNXMLDetailBuilder(bool EncodeXMLStringToUTF16)
    {
        m_ht = m_DefHT;
        m_EncodeXMLToUTF16 = EncodeXMLStringToUTF16;
    }

    public bool EncodeXMLToUTF16
    {
        get
        {
            return m_EncodeXMLToUTF16;
        }
    }

    public void AddNewRecord()
    {
        m_ht = new XMLElement();
        m_Records.Add(m_ht);
    }

    public void Add(string key, string Value)
    {
        try
        {
            m_ht.Elements.Add(key, Value);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string GetValue(string key)
    {
        try
        {
            string valueElement = "";

            if (m_ht.Elements.ContainsKey(key))
                valueElement = (string)m_ht.Elements[key];
            else
                valueElement = "";

            return valueElement;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void update(string key, string Value)
    {
        try
        {
            if (m_ht.Elements.ContainsKey(key))
                m_ht.Elements[key] = Value;
            else
                m_ht.Elements.Add(key, Value);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void AddAttribute(string key, string Value)
    {
        try
        {
            m_ht.Attributes.Add(key, Value);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string GetAttributeValue(string key)
    {
        try
        {
            string valueElement = "";

            if (m_ht.Attributes.ContainsKey(key))
                valueElement = (string)m_ht.Attributes[key];
            else
                valueElement = "";

            return valueElement;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string ToXMLString()
    {
        System.IO.StringWriter Sw = new System.IO.StringWriter();
        System.Xml.XmlTextWriter Xmlw = new System.Xml.XmlTextWriter(Sw);

        try
        {
            if (m_EncodeXMLToUTF16)
                Xmlw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-16\"");
            else
                Xmlw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"ISO-8859-1\"");

            Xmlw.WriteStartElement("DT");

            foreach (string key in m_DefHT.Elements.Keys)
            {
                // Open the D Tag
                Xmlw.WriteStartElement("D");

                // Write the N attribute with the key
                Xmlw.WriteAttributeString("N", key);

                // Write the actual Value and close the D Tag
                Xmlw.WriteCData((string)m_DefHT.Elements[key]);
                Xmlw.WriteEndElement();
            }

            foreach (XMLElement ht in this.m_Records)
            {
                Xmlw.WriteStartElement("CDT");

                foreach (string Attr in ht.Attributes.Keys)
                {
                    Xmlw.WriteAttributeString(Attr, (string)ht.Attributes[Attr]);
                }

                foreach (string key in ht.Elements.Keys)
                {
                    // Open the D Tag
                    Xmlw.WriteStartElement("D");

                    // Write the N attribute with the key
                    Xmlw.WriteAttributeString("N", key);

                    // Write the actual Value and close the D Tag
                    Xmlw.WriteCData((string)ht.Elements[key]);
                    Xmlw.WriteEndElement();
                }

                Xmlw.WriteEndElement();
            }

            Xmlw.WriteEndElement();

            return Sw.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
