﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Grean.AtomEventStore
{
    public class AtomEntry : IXmlWritable
    {
        private readonly UuidIri id;
        private readonly string title;
        private readonly DateTimeOffset published;
        private readonly DateTimeOffset updated;
        private readonly AtomAuthor author;
        private readonly XmlAtomContent content;
        private readonly IEnumerable<AtomLink> links;

        public AtomEntry(
            UuidIri id,
            string title, 
            DateTimeOffset published,
            DateTimeOffset updated,
            AtomAuthor author,
            XmlAtomContent content,
            IEnumerable<AtomLink> links)
        {
            this.id = id;
            this.title = title;
            this.published = published;
            this.updated = updated;
            this.author = author;
            this.content = content;
            this.links = links;
        }
        
        public UuidIri Id
        {
            get { return this.id; }
        }

        public string Title
        {
            get { return this.title; }
        }

        public DateTimeOffset Published
        {
            get { return this.published; }
        }

        public DateTimeOffset Updated
        {
            get { return this.updated; }
        }

        public AtomAuthor Author
        {
            get { return this.author; }
        }

        public XmlAtomContent Content
        {
            get { return this.content; }
        }

        public IEnumerable<AtomLink> Links
        {
            get { return this.links; }
        }

        public AtomEntry WithTitle(string newTitle)
        {
            return new AtomEntry(
                this.id,
                newTitle,
                this.published,
                this.updated,
                this.author,
                this.content,
                this.links);
        }

        public AtomEntry WithUpdated(DateTimeOffset newUpdated)
        {
            return new AtomEntry(
                this.id,
                this.title,
                this.published,
                newUpdated,
                this.author,
                this.content,
                this.links);
        }

        public AtomEntry WithAuthor(AtomAuthor newAuthor)
        {
            return new AtomEntry(
                this.id,
                this.title,
                this.published,
                this.updated,
                newAuthor,
                this.content,
                this.links);
        }

        public AtomEntry WithContent(XmlAtomContent newContent)
        {
            return new AtomEntry(
                this.id,
                this.title,
                this.published,
                this.updated,
                this.author,
                newContent,
                this.links);
        }

        public AtomEntry WithLinks(IEnumerable<AtomLink> newLinks)
        {
            return new AtomEntry(
                this.id,
                this.title,
                this.published,
                this.updated,
                this.author,
                this.content,
                newLinks);
        }

        public void WriteTo(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("entry", "http://www.w3.org/2005/Atom");

            xmlWriter.WriteElementString("id", this.id.ToString());

            xmlWriter.WriteStartElement("title");
            xmlWriter.WriteAttributeString("type", "text");
            xmlWriter.WriteString(this.title);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteElementString("published", this.published.ToString("o"));

            xmlWriter.WriteElementString("updated", this.updated.ToString("o"));

            this.author.WriteTo(xmlWriter);

            this.WriteLinksTo(xmlWriter);

            this.content.WriteTo(xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private void WriteLinksTo(XmlWriter xmlWriter)
        {
            foreach (var l in this.links)
                l.WriteTo(xmlWriter);
        }
    }
}
