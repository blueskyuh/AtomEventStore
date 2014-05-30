﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;
using Xunit.Extensions;

namespace Grean.AtomEventStore.UnitTests
{
    public class ConventionBasedSerializerOfComplexImmutableClassesTests
    {
        [Theory, AutoAtomData]
        public void SutCanSerializeNestedItem(
            XmlAtomContent seed,
            Envelope<TestEventX> env)
        {
            var sut = seed.WithItem(env);

            var actual = sut.ToXmlString(
                new ConventionBasedSerializerOfComplexImmutableClasses());

            var expected = XDocument.Parse(
                "<content type=\"application/xml\" xmlns=\"http://www.w3.org/2005/Atom\">" +
                "  <envelope xmlns=\"urn:grean:atom-event-store:unit-tests\">" +
                "    <id>urn:uuid:" + env.Id + "</id>" +
                "    <item>" +
                "      <test-event-x>" +
                "        <number>" + env.Item.Number + "</number>" +
                "        <text>" + env.Item.Text + "</text>" +
                "      </test-event-x>" +
                "    </item>" +
                "  </envelope>" +
                "</content>");
            Assert.Equal(expected, XDocument.Parse(actual), new XNodeEqualityComparer());
        }
    }
}
