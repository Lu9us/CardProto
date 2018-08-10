using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Api;
using Util.Interfaces;

namespace Util.Tests
{
    [TestFixture]
    public class SerilizationTest
    {
        [Test]
        public void SerilizeDeserilizeTest()
        {
            ISerizilizer serilizer = new Serilizer();
            DataMapManager dataMapManager = new DataMapManager();
            dataMapManager.CreateNewMap();
            dataMapManager.getCurrentMap().AddData("Test:1", 1);
            dataMapManager.getCurrentMap().AddData("Test:2", 2);
            byte[] data = serilizer.Serilize<DataMap>(dataMapManager.getCurrentMap());
            Packet packet = new Packet();
            packet.data = data;
            packet.length = data.Length;
            data  = serilizer.Serilize<Packet>(packet);
            Packet p = serilizer.DeSerilize<Packet>(data);
            Assert.NotNull(p);
            Assert.NotNull(serilizer.DeSerilize<DataMap>(p.data));
            data = serilizer.Serilize<DataMap>(dataMapManager.getCurrentMap());
             packet = new Packet();
            packet.data = data;
            packet.length = data.Length;
            data = serilizer.Serilize<Packet>(packet);
            p = serilizer.DeSerilize<Packet>(data);
            Assert.NotNull(p);
            Assert.NotNull(serilizer.DeSerilize<DataMap>(p.data));
        }
        [Test]
        public void JSONSerilizationTest()
        {
            ISerizilizer serilizer = new JSONSerilizer();
            DataMapManager dataMapManager = new DataMapManager();
            dataMapManager.CreateNewMap();
            dataMapManager.getCurrentMap().AddData("Test:1", 1);
            dataMapManager.getCurrentMap().AddData("Test:2", 2);
            byte[] data = serilizer.Serilize<DataMap>(dataMapManager.getCurrentMap());
            Packet packet = new Packet();
            packet.data = data;
            packet.length = data.Length;
            data = serilizer.Serilize<Packet>(packet);
            Packet p = serilizer.DeSerilize<Packet>(data);
            Assert.NotNull(p);
            Assert.NotNull(serilizer.DeSerilize<DataMap>(p.data));
            data = serilizer.Serilize<DataMap>(dataMapManager.getCurrentMap());
            packet = new Packet();
            packet.data = data;
            packet.length = data.Length;
            data = serilizer.Serilize<Packet>(packet);
            p = serilizer.DeSerilize<Packet>(data);
            Assert.NotNull(p);
            Assert.NotNull(serilizer.DeSerilize<DataMap>(p.data));
        }
    }
}
