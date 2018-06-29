using System.Dynamic;
using Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionTests
{
    [TestClass]
    public class DynamicExtensionTests
    {
        [TestMethod]
        public void HasPropertyShouldReturnFalseWhenDynamicObjectIsNull()
        {
            dynamic obj = null;
            Assert.IsFalse(DynamicExtensions.HasProperty(obj, "property"));
        }

        [TestMethod]
        public void HasPropertyShouldReturnTrueWhenPropertyExistsAndFalseWhenPropertyDontExist()
        {
            dynamic obj = new ExpandoObject();
            obj.property = "Hola";
            Assert.IsTrue(DynamicExtensions.HasProperty(obj, "property"));
            Assert.IsFalse(DynamicExtensions.HasProperty(obj, "inexistentproperty"));

            dynamic obj2 = new CustomClassForDynamicTest();
            Assert.IsTrue(DynamicExtensions.HasProperty(obj2, "Value"));
            Assert.IsFalse(DynamicExtensions.HasProperty(obj2, "inexistentproperty"));
        }

        [TestMethod]
        public void AsIntShouldReturnIntValueWhenIsNumericAndZeroInOtherCases()
        {
            dynamic obj = new ExpandoObject();
            obj.entero = 1;
            obj.doble = (double)2;
            obj.cadena = "cadena";

            Assert.AreEqual(DynamicExtensions.AsInt(obj.entero), 1);
            Assert.AreEqual(DynamicExtensions.AsInt(obj.doble), 2);
            Assert.AreEqual(DynamicExtensions.AsInt(obj.cadena), 0);
        }
    }
}
