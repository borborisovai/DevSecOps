using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderPricing;

namespace OrderPricing.Tests;

[TestClass]
public class PriceCalculatorTests
{
    // Обычный заказ
    [TestMethod]
    public void RegularOrder_1000WithoutLoyalty_TotalIs1300()
    {
        // Arrange: данные и ожидание, рассчитанное по требованиям.
        decimal subtotal = 1000m;
        decimal expected = 1300m;

        // Act: один вызов проверяемого поведения.
        decimal actual = PriceCalculator.CalculateTotal(subtotal, false, false);

        // Assert: сравнение ожидания и результата.
        Assert.AreEqual(expected, actual);
    }

    // Заказ с 5% скидкой
    [TestMethod]
    public void RegularOrder_5000WithoutLoyalty_TotalIs4750()
    {
        // Arrange: данные и ожидание, рассчитанное по требованиям.
        decimal subtotal = 5000m;
        decimal expected = 4750m;

        // Act: один вызов проверяемого поведения.
        decimal actual = PriceCalculator.CalculateTotal(subtotal, false, false);

        // Assert: сравнение ожидания и результата.
        Assert.AreEqual(expected, actual);
    }

    // Обычный заказ с картой лояльности
    [TestMethod]
    public void RegularOrder_1000WithLoyalty_TotalIs1200()
    {
        // Arrange: данные и ожидание, рассчитанное по требованиям.
        decimal subtotal = 1000m;
        decimal expected = 1200m;

        // Act: один вызов проверяемого поведения.
        decimal actual = PriceCalculator.CalculateTotal(subtotal, true, false);

        // Assert: сравнение ожидания и результата.
        Assert.AreEqual(expected, actual);
    }

    // Заказ с отрицательной стоимостью
    [TestMethod]
    public void IllegalOrger_NegativeSubtotal()
    {
        // Arrange: данные и ожидание, рассчитанное по требованиям.
        decimal subtotal = -1000m;
        //decimal expected = 0m;

        // Act: один вызов проверяемого поведения.


        // Assert: сравнение ожидания и результата.
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => PriceCalculator.CalculateTotal(subtotal, true, false));
    }

    // Заказ стоимостью больше 100000
    [TestMethod]
    public void IllegalOrger_TooBigSubtotal()
    {
        // Arrange: данные и ожидание, рассчитанное по требованиям.
        decimal subtotal = 1000001m;
        //decimal expected = 0m;

        // Act: один вызов проверяемого поведения.


        // Assert: сравнение ожидания и результата.
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => PriceCalculator.CalculateTotal(subtotal, true, false));
    }

    // Бесплатный заказ
    [TestMethod]
    public void RegularOrder_0WithoutLoyalty_TotalIs300()
    {
        // Arrange: данные и ожидание, рассчитанное по требованиям.
        decimal subtotal = 0m;
        decimal expected = 300m;

        // Act: один вызов проверяемого поведения.
        decimal actual = PriceCalculator.CalculateTotal(subtotal, false, false);

        // Assert: сравнение ожидания и результата.
        Assert.AreEqual(expected, actual);
    }

    // Бесплатный заказ с картой
    [TestMethod]
    public void RegularOrder_0WithLoyalty_TotalIs300()
    {
        // Arrange: данные и ожидание, рассчитанное по требованиям.
        decimal subtotal = 0m;
        decimal expected = 300m;

        // Act: один вызов проверяемого поведения.
        decimal actual = PriceCalculator.CalculateTotal(subtotal, true, false);

        // Assert: сравнение ожидания и результата.
        Assert.AreEqual(expected, actual);
    }

    // Бесплатный экспресс заказ
    [TestMethod]
    public void RegularOrder_0WithLoyalty_Express_TotalIs500()
    {
        // Arrange: данные и ожидание, рассчитанное по требованиям.
        decimal subtotal = 0m;
        decimal expected = 500m;

        // Act: один вызов проверяемого поведения.
        decimal actual = PriceCalculator.CalculateTotal(subtotal, false, true);

        // Assert: сравнение ожидания и результата.
        Assert.AreEqual(expected, actual);
    }

    // Порог стоимости заказа без карты и экспресс
    [TestMethod]
    public void RegularOrder_100000WithoutLoyalty_TotalIs95000()
    {
        // Arrange: данные и ожидание, рассчитанное по требованиям.
        decimal subtotal = 100000m;
        decimal expected = 95000m;

        // Act: один вызов проверяемого поведения.
        decimal actual = PriceCalculator.CalculateTotal(subtotal, false, false);

        // Assert: сравнение ожидания и результата.
        Assert.AreEqual(expected, actual);
    }

    // Чуть ниже порога бесплатной доставки
    [TestMethod]
    public void RegularOrder_3999WithoutLoyalty_TotalIs4299()
    {
        // Arrange: данные и ожидание, рассчитанное по требованиям.
        decimal subtotal = 3999.99m;
        decimal expected = 4299.99m;

        // Act: один вызов проверяемого поведения.
        decimal actual = PriceCalculator.CalculateTotal(subtotal, true, true);

        // Assert: сравнение ожидания и результата.
        Assert.AreEqual(expected, actual);
    }

    // ровно порог бесплатной доставки — доставка бесплатна
    [TestMethod]
    public void RegularOrder_4000WithoutLoyalty_TotalIs4000()
    {
        decimal subtotal = 4000m;
        decimal expected = 4000m;

        decimal actual = PriceCalculator.CalculateTotal(subtotal, false, false);

        Assert.AreEqual(expected, actual);
    }

    // R2, R4: чуть ниже порога 5% — скидки по сумме нет, доставка бесплатна
    [TestMethod]
    public void RegularOrder_4999_99WithoutLoyalty_TotalIs4999_99()
    {
        decimal subtotal = 4999.99m;
        decimal expected = 4999.99m;

        decimal actual = PriceCalculator.CalculateTotal(subtotal, false, false);

        Assert.AreEqual(expected, actual);
    }

    // R3, R6: скидки не складываются, применяется большая — 10%
    [TestMethod]
    public void RegularOrder_5000WithLoyalty_TotalIs4500()
    {
        decimal subtotal = 5000m;
        decimal expected = 4500m;

        decimal actual = PriceCalculator.CalculateTotal(subtotal, true, false);

        Assert.AreEqual(expected, actual);
    }

    // R3, R4, R6: округление скидки 10% вверх, затем доставка 300
    [TestMethod]
    public void RegularOrder_3333_33WithLoyalty_TotalIs3300()
    {
        decimal subtotal = 3333.33m;
        decimal expected = 3300m;

        decimal actual = PriceCalculator.CalculateTotal(subtotal, true, false);

        Assert.AreEqual(expected, actual);
    }

    // R3, R4, R6: округление скидки 10%, порог доставки определяется до скидки
    [TestMethod]
    public void RegularOrder_4444_44WithLoyalty_TotalIs4000()
    {
        decimal subtotal = 4444.44m;
        decimal expected = 4000m;

        decimal actual = PriceCalculator.CalculateTotal(subtotal, true, false);

        Assert.AreEqual(expected, actual);
    }

    // R3, R5, R6: округление скидки 10%, затем экспресс 500
    [TestMethod]
    public void ExpressOrder_5000_01WithLoyalty_TotalIs5000_01()
    {
        decimal subtotal = 5000.01m;
        decimal expected = 5000.01m;

        decimal actual = PriceCalculator.CalculateTotal(subtotal, true, true);

        Assert.AreEqual(expected, actual);
    }

    // R1: точная верхняя граница 100000.01 — исключение
    [TestMethod]
    public void Subtotal100000_01_ThrowsArgumentOutOfRangeException()
    {
        decimal subtotal = 100000.01m;

        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => PriceCalculator.CalculateTotal(subtotal, false, false));
    }

    // R1: точная нижняя граница -0.01 — исключение
    [TestMethod]
    public void SubtotalMinus0_01_ThrowsArgumentOutOfRangeException()
    {
        decimal subtotal = -0.01m;

        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => PriceCalculator.CalculateTotal(subtotal, false, false));
    }

    // R5: экспресс заменяет бесплатную обычную доставку
    [TestMethod]
    public void ExpressOrder_4000WithoutLoyalty_TotalIs4500()
    {
        decimal subtotal = 4000m;
        decimal expected = 4500m;

        decimal actual = PriceCalculator.CalculateTotal(subtotal, false, true);

        Assert.AreEqual(expected, actual);
    }

    // R3, R5: карта 10% и экспресс 500
    [TestMethod]
    public void ExpressOrder_4000WithLoyalty_TotalIs4100()
    {
        decimal subtotal = 4000m;
        decimal expected = 4100m;

        decimal actual = PriceCalculator.CalculateTotal(subtotal, true, true);

        Assert.AreEqual(expected, actual);
    }
}
